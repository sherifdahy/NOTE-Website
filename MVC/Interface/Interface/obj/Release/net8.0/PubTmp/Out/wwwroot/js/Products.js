



const page = (function () {
    function init() {
        document.querySelector('.fa-cart-shopping').parentElement.classList.add('active');
        document.querySelector('#deleteAllBtn').addEventListener('click', deleteAll);
        document.querySelector('#delete').addEventListener('click', deleteItem);
    }

    function deleteItem(event) {
        event.preventDefault();
        Swal.fire({
            title: "هل انت متأكد من حذف السلعة والخدمة",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Confirm"
        }).then((result) => {
            if (result.isConfirmed) {
                location.href = this.href;
            }
        });
    }
    function deleteAll(event) {
        event.preventDefault();
        Swal.fire({
            title: "هل انت متأكد من حذف جميع السلع والخدمات",
            text: "لن تكون قادر علي استرجاع السلع والخدمات للأبد",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Confirm"
        }).then((result) => {
            if (result.isConfirmed) {
                location.href = this.href;
            }
        });
    }

    function Edit(val) {
        $.ajax({
            url: "/products/edit",
            method: "get",
            data: { "id": val },
            success: function (result) {

                document.querySelector('.modal-body').innerHTML = result;
                $('#form').modal('show'); 
            },
            error: function (xhr, status) {
                alert("Error: ", status, xhr);
            }
        });
    }
    function confirmEdit(event,id) {
        event.preventDefault();
        $.ajax({
            url: "/products/edit/" + id,
            method: "post",
            data: $('#formEdit').serialize(), // get value from tag input and serialize it
            success: function (result) {
                if (result.success) {
                    location.href = "/products/index";
                }
                document.querySelector('.modal-body').innerHTML = result;
                $('#form').modal('show');
            },
            error: function (xhr, status) {
                alert("Error: " + status + " " + xhr.statusText);
            }
        });
    }



    function Add() {
        $.ajax({
            url: "/products/add",
            method: "get",
            success: function (result) {

                document.querySelector('.modal-body').innerHTML = result;
                $('#form').modal('show');
            },
            error: function (xhr, status) {
                alert("Error: ", status, xhr);
            }
        });
    }
    
    function confirmAdd(event) {
        event.preventDefault();
        $.ajax({
            url: "/products/add",
            method: "post",
            data: $('#formAdd').serialize(),
            success: function (result) {
                if (result.success) {
                    location.href = "/products/index";
                }
                document.querySelector('.modal-body').innerHTML = result;
                $('#form').modal('show');
            },
            error: function (xhr, status) {
                alert("Error: " + status + " " + xhr.statusText);
            }
        });
    }


    return {
        init,
        Edit,
        Add,
        confirmEdit,
        confirmAdd,
    }

})();

document.addEventListener('DOMContentLoaded', page.init);





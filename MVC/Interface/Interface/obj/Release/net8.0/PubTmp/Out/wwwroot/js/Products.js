



const page = (function () {
    function init() {
        document.querySelector('.fa-cart-shopping').parentElement.classList.add('active');
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





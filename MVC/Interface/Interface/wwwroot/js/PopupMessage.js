const SweetAlert = (function () {
    const Toast = Swal.mixin({
        toast: true,
        position: "bottom-end",
        width: 500,
        height:300,
        showConfirmButton: false,
        timer: 5000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.onmouseenter = Swal.stopTimer;
            toast.onmouseleave = Swal.resumeTimer;
        }
    });
    function notifications(collection) {
        for (note of collection) {
            notification(note.Icon,note.title, note.Message);
        }
    }
    function notification(icon, title,message) {
        Toast.fire({
            icon: icon,
            title: title,
            text: message
        });
    }
    function message(MessageVM) {
        Swal.fire({
            icon: MessageVM.Icon,
            title: MessageVM.Title,
            text: MessageVM.Message,
        });
    }
    function confirm(event,title,text,icon) {
        event.preventDefault();
        Swal.fire({
            title: title,
            text: text,
            icon: icon,
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "تأكيد",
            cancelButtonText: "إلغاء"
        }).then((result) => {
            if (result.isConfirmed) {
                let tag = event.target.tagName.toLowerCase();
                if (tag == 'form') {
                    event.target.submit();
                }
                else if (tag=== 'a') {
                    window.location.href = event.target.href;
                }
            }
        });
    }
    return {
        confirm,
        message,
        notification,
        notifications
    }
})();


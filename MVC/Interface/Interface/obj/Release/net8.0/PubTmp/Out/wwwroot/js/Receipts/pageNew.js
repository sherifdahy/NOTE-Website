document.querySelector('.new-receipt').parentNode.classList.add('active');
document.querySelector('.new-receipt').parentNode.parentNode.classList.toggle('d-none');



document.addEventListener('DOMContentLoaded', function () {
    var now = new Date();
    var timezoneOffset = now.getTimezoneOffset() * 60000;
    var localISOTime = (new Date(now - timezoneOffset)).toISOString().slice(0, 16);
    document.getElementById('DateTime').value = localISOTime;
});


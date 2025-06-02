// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function vuhodOnStart(event) {
    event.preventDefault();
    window.location.replace('/HomePage/Index');

}

var sideModal = document.getElementById('sideMenu');
var overlay = document.getElementById('overlay');

function openPopup() {
	window.scrollTo({
		top: 0,
		behavior: 'smooth'
	});
	document.body.style.overflow = 'hidden';

    document.querySelector('.popup').style.display = 'block';
    document.getElementById('overlay').style.display = 'block';

    document.querySelector('.popup').style.width = '300px';
}
function closePopup() {
    var inputs = document.getElementsByTagName('input');
    var textError = document.querySelectorAll('.text-danger');
    textError.forEach(child => {
        child.textContent = '';
    });
    for (let i = 0; i < inputs.length; i++) {
        inputs[i].textContent = null;
        inputs[i].style.borderBottom = '0';
    }
	document.body.style.overflow = 'auto';

	document.querySelector('.popup').style.display = 'none';
    document.getElementById('overlay').style.display = 'none';

    const forms = document.querySelectorAll('.popup form');
    forms.forEach(form => form.reset());
}

function profilOpen() {
    document.body.style.overflow = 'hidden';
    document.getElementById('overlay').style.display = 'block';
    sideModal.style.visibility = 'visible';
    overlay.style.cursor = 'pointer';

     startLogin = document.getElementById('portfile_mail').value;
     startPhone = document.getElementById('portfile_phone').value;

 }
function profilClose() {

    overlay.style.cursor = 'auto';
     document.body.style.overflow = 'auto';
     document.getElementById('overlay').style.display = 'none';
     sideModal.style.visibility = 'collapse';

     document.getElementById('portfile_mail').disabled = true;
     document.getElementById('portfile_phone').disabled = true;

    document.getElementById('portfile_mail').style.borderBottom = '0';
    document.getElementById('portfile_phone').style.borderBottom = '0';

     document.getElementById('portfile_save_btn').style.display = 'none';

     document.getElementById('portfile_mail').value = startLogin;
     document.getElementById('portfile_phone').value = startPhone;
}

window.onclick = function (event) {
    if (sideModal != null) {
        if (event.target == overlay && sideModal.style.visibility == 'visible') {
            profilClose();
        }
    }
};
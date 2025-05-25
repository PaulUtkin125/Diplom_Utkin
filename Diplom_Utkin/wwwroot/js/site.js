// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function vuhodOnStart(event) {
    event.preventDefault();
    window.location.replace('/HomePage/Index');

}

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
	document.body.style.overflow = 'auto';

	document.querySelector('.popup').style.display = 'none';
    document.getElementById('overlay').style.display = 'none';

    const forms = document.querySelectorAll('.popup form');
    forms.forEach(form => form.reset());
}

function profilOpen() {
    document.body.style.overflow = 'hidden';
    document.getElementById('overlay').style.display = 'block';
    document.getElementById('sideMenu').style.visibility = 'visible';

     startLogin = document.getElementById('portfile_mail').value;
     startPhone = document.getElementById('portfile_phone').value;
 }
 function profilClose (){
     document.body.style.overflow = 'auto';
     document.getElementById('overlay').style.display = 'none';
     document.getElementById('sideMenu').style.visibility = 'collapse';

     document.getElementById('portfile_mail').disabled = true;
     document.getElementById('portfile_phone').disabled = true;
     document.getElementById('portfile_save_btn').style.display = 'none';

     document.getElementById('portfile_mail').value = startLogin;
     document.getElementById('portfile_phone').value = startPhone;
}

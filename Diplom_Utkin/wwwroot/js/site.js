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
}
function closePopup() {
	document.body.style.overflow = 'auto';

	document.querySelector('.popup').style.display = 'none';
	document.getElementById('overlay').style.display = 'none';
}

 function profilOpen (){
       document.body.style.overflow = 'hidden';
       document.getElementById('overlay').style.display = 'block';
       document.getElementById('sideMenu').style.visibility = 'visible';
 }
 function profilClose (){
     document.body.style.overflow = 'auto';
     document.getElementById('overlay').style.display = 'none';
     document.getElementById('sideMenu').style.visibility = 'collapse';
}

async function loadAllUserType() {

    const response = await fetch('http://localhost:5189/api/Admin/AllUserType', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });
    if (!response.ok) {
        throw new Error('Network response was not ok');
    }
    var UserTypeList = response.json();
    return UserTypeList;
}

async function updateUser(id_, mail_, idRole_, phone_) {
   
    const response = await fetch('http://localhost:5189/api/Admin/updateUser', {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id: id_, Loggin: mail_, Phone: phone_, TypeOfUserId: idRole_ })
    });
    if (!response.ok) {
        throw new Error('Network response was not ok');
    }
    const data = response.json();
    console.log('User updated successfully:', data);
}
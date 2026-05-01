function switchRole(role) {
    // Remove active class from both
    document.getElementById('btnStudent').classList.remove('active');
    document.getElementById('btnTeacher').classList.remove('active');

    // Add active class to selected and update hidden input
    if (role === 'student') {
        document.getElementById('btnStudent').classList.add('active');
        document.getElementById('btnTeacher').classList.remove('active');
        document.getElementById('SelectedRole').value = 'Student';
    } else {
        document.getElementById('btnTeacher').classList.add('active');
        document.getElementById('btnStudent').classList.remove('active');
        document.getElementById('SelectedRole').value = 'Teacher';
    }

    // Optional: Add a small scale-up animation
    const activeBtn = document.querySelector('.role-btn.active');
    activeBtn.style.transform = "scale(1.1)";
    setTimeout(() => { activeBtn.style.transform = "scale(1)"; }, 150);
}
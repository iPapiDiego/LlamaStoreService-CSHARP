
document.addEventListener('DOMContentLoaded', function () {
    const loginForm = document.querySelector('.login-form');
    const loginButton = document.getElementById('login-button');

    loginForm.addEventListener('submit', function (e) {
        e.preventDefault();

        // Simular carga
        loginButton.disabled = true;
        loginButton.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Procesando...';

        // Simular retraso de red
        setTimeout(() => {
            // Aquí iría tu lógica real de autenticación
            alert('¡Inicio de sesión exitoso!');
            loginButton.disabled = false;
            loginButton.textContent = 'Ingresar';

            // Redirección simulada
            // window.location.href = '/dashboard.html';
        }, 1500);
    });

    // Efecto hover mejorado para inputs
    const inputs = document.querySelectorAll('input');
    inputs.forEach(input => {
        input.addEventListener('focus', function () {
            this.parentElement.querySelector('i').style.color = '#0077ff';
        });

        input.addEventListener('blur', function () {
            this.parentElement.querySelector('i').style.color = 'rgba(255, 255, 255, 0.7)';
        });
    });
});

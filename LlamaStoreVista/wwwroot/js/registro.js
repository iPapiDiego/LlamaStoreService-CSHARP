document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('registerForm');
    const registerButton = document.getElementById('register-button');
    const passwordInput = document.getElementById('clave');
    const confirmPasswordInput = document.getElementById('confirmar');
    const passwordStrength = document.getElementById('password-strength');
    const strengthBar = document.getElementById('strength-bar');

    // Mostrar fortaleza de la contraseña
    passwordInput.addEventListener('input', function () {
        const password = this.value;
        passwordStrength.style.display = password ? 'block' : 'none';

        // Calcular fortaleza
        let strength = 0;
        if (password.length >= 8) strength += 1;
        if (password.match(/[a-z]/)) strength += 1;
        if (password.match(/[A-Z]/)) strength += 1;
        if (password.match(/[0-9]/)) strength += 1;
        if (password.match(/[^a-zA-Z0-9]/)) strength += 1;

        // Actualizar barra
        const width = strength * 20;
        let color = '#ff4444'; // Rojo
        if (strength >= 4) color = '#4CAF50'; // Verde
        else if (strength >= 3) color = '#FFC107'; // Amarillo

        strengthBar.style.width = width + '%';
        strengthBar.style.background = color;
    });

    // Validación en tiempo real
    form.addEventListener('input', function (e) {
        const input = e.target;
        const errorElement = document.getElementById(input.id + '-error');

        if (input.validity.valid) {
            errorElement.style.display = 'none';
            input.style.border = 'none';
        } else {
            // No mostrar error hasta que el usuario haya interactuado
            if (input.value) {
                errorElement.style.display = 'block';
                input.style.border = '1px solid #ff6b6b';
            }
        }

        // Validación especial para confirmar contraseña
        if (input.id === 'confirmar' && passwordInput.value !== input.value && input.value) {
            errorElement.style.display = 'block';
            input.style.border = '1px solid #ff6b6b';
        }
    });

    // Validación al enviar el formulario
    form.addEventListener('submit', function (e) {
        e.preventDefault();
        let isValid = true;

        // Validar DNI
        const dniInput = document.getElementById('dni');
        if (!/^\d{8}$/.test(dniInput.value)) {
            showError(dniInput);
            isValid = false;
        }

        // Validar nombre y apellidos
        const nameInputs = ['nombre', 'apellidos'];
        nameInputs.forEach(id => {
            const input = document.getElementById(id);
            if (!/^[A-Za-zÁ-ú\s]{2,}$/.test(input.value)) {
                showError(input);
                isValid = false;
            }
        });

        // Validar fecha (mayor de 18 años)
        const fechaInput = document.getElementById('fecha');
        const birthDate = new Date(fechaInput.value);
        const minDate = new Date();
        minDate.setFullYear(minDate.getFullYear() - 18);

        if (birthDate > minDate) {
            showError(fechaInput);
            isValid = false;
        }

        // Validar país seleccionado
        const paisInput = document.getElementById('pais');
        if (!paisInput.value) {
            showError(paisInput);
            isValid = false;
        }

        // Validar email
        const emailInput = document.getElementById('correo');
        if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailInput.value)) {
            showError(emailInput);
            isValid = false;
        }

        // Validar contraseña
        const password = passwordInput.value;
        if (password.length < 8 || !/[a-z]/.test(password) || !/[A-Z]/.test(password) || !/[0-9]/.test(password)) {
            showError(passwordInput);
            isValid = false;
        }

        // Validar confirmación de contraseña
        if (password !== confirmPasswordInput.value) {
            showError(confirmPasswordInput);
            isValid = false;
        }

        // Si todo es válido, simular envío
        if (isValid) {
            registerButton.disabled = true;
            registerButton.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Registrando...';

            // Simular envío al servidor
            setTimeout(() => {
                alert('¡Registro exitoso! Bienvenido/a');
                registerButton.disabled = false;
                registerButton.innerHTML = '<i class="fas fa-user-plus"></i> Registrarse';
                // form.submit(); // Descomentar para envío real
            }, 1500);
        } else {
            // Efecto de "shake" para indicar error
            form.style.animation = 'shake 0.4s';
            setTimeout(() => {
                form.style.animation = '';
            }, 400);
        }
    });

    function showError(input) {
        const errorElement = document.getElementById(input.id + '-error');
        errorElement.style.display = 'block';
        input.style.border = '1px solid #ff6b6b';
    }
});
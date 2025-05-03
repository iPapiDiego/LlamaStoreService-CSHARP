
document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('profileForm');
    const updateButton = document.getElementById('update-button');
    const passwordInput = document.getElementById('clave');
    const confirmPasswordInput = document.getElementById('confirmar');
    const passwordStrength = document.getElementById('password-strength');
    const strengthBar = document.getElementById('strength-bar');
    const successMessage = document.getElementById('success-message');

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
        let color = '#ef233c'; // Rojo
        if (strength >= 4) color = '#2ec4b6'; // Verde
        else if (strength >= 3) color = '#ffbe0b'; // Amarillo

        strengthBar.style.width = width + '%';
        strengthBar.style.background = color;
    });

    // Validación en tiempo real
    form.addEventListener('input', function (e) {
        const input = e.target;
        const errorElement = document.getElementById(input.id + '-error');

        if (input.validity.valid) {
            errorElement.style.display = 'none';
            input.style.borderColor = 'rgba(0, 0, 0, 0.1)';
        } else {
            // No mostrar error hasta que el usuario haya interactuado
            if (input.value) {
                errorElement.style.display = 'block';
                input.style.borderColor = 'var(--error-color)';
            }
        }

        // Validación especial para confirmar contraseña
        if (input.id === 'confirmar' && passwordInput.value !== input.value && input.value) {
            errorElement.style.display = 'block';
            input.style.borderColor = 'var(--error-color)';
        }
    });

    // Validación al enviar el formulario
    form.addEventListener('submit', function (e) {
        e.preventDefault();
        let isValid = true;

        // Validar nombre y apellido
        const nameInputs = ['nombre', 'apellido'];
        nameInputs.forEach(id => {
            const input = document.getElementById(id);
            if (!/^[A-Za-zÁ-ú\s]{2,}$/.test(input.value)) {
                showError(input);
                isValid = false;
            }
        });

        // Validar fecha (mayor de 18 años)
        const fechaInput = document.getElementById('fnacim');
        const birthDate = new Date(fechaInput.value);
        const minDate = new Date();
        minDate.setFullYear(minDate.getFullYear() - 18);

        if (birthDate > minDate) {
            showError(fechaInput);
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
            updateButton.disabled = true;
            updateButton.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Actualizando...';

            // Simular envío al servidor
            setTimeout(() => {
                successMessage.style.display = 'block';
                updateButton.disabled = false;
                updateButton.innerHTML = '<i class="fas fa-save"></i> Actualizar';

                // Ocultar mensaje después de 3 segundos
                setTimeout(() => {
                    successMessage.style.display = 'none';
                }, 3000);

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
        input.style.borderColor = 'var(--error-color)';
    }
});

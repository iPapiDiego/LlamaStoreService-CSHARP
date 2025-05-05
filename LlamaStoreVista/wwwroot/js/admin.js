document.addEventListener('DOMContentLoaded', function () {
    // Navegación entre secciones
    const navItems = document.querySelectorAll('.admin-nav li');
    const sections = document.querySelectorAll('.content-section');
    const sectionTitles = document.querySelectorAll('#section-title');

    navItems.forEach(item => {
        item.addEventListener('click', function () {
            const sectionId = this.getAttribute('data-section');

            // Actualizar navegación activa
            navItems.forEach(nav => nav.classList.remove('active'));
            this.classList.add('active');

            // Mostrar sección correspondiente
            sections.forEach(section => {
                section.classList.remove('active');
                if (section.id === `${sectionId}-section`) {
                    section.classList.add('active');
                }
            });

            // Actualizar título
            sectionTitles.forEach(title => {
                title.textContent = this.textContent.trim();
            });
        });
    });

    // Modales
    const modals = {
        product: {
            btn: document.getElementById('add-product-btn'),
            modal: document.getElementById('product-modal'),
            close: document.querySelector('#product-modal .close-modal'),
            cancel: document.getElementById('cancel-product')
        },
        accessory: {
            btn: document.getElementById('add-accessory-btn'),
            modal: document.getElementById('accessory-modal'),
            close: document.querySelector('#accessory-modal .close-modal'),
            cancel: document.getElementById('cancel-accessory')
        },
        user: {
            btn: document.getElementById('add-user-btn'),
            modal: document.getElementById('user-modal'),
            close: document.querySelector('#user-modal .close-modal'),
            cancel: document.getElementById('cancel-user')
        },
        sale: {
            btns: document.querySelectorAll('.view-btn'),
            modal: document.getElementById('sale-modal'),
            close: document.querySelector('#sale-modal .close-modal'),
            cancel: document.querySelector('.close-sale-modal')
        }
    };

    // Funciones para manejar modales
    function setupModal(modalConfig) {
        if (modalConfig.btn) {
            modalConfig.btn.addEventListener('click', () => {
                modalConfig.modal.classList.add('active');
            });
        }

        if (modalConfig.close) {
            modalConfig.close.addEventListener('click', () => {
                modalConfig.modal.classList.remove('active');
            });
        }

        if (modalConfig.cancel) {
            modalConfig.cancel.addEventListener('click', () => {
                modalConfig.modal.classList.remove('active');
            });
        }

        // Cerrar modal al hacer clic fuera del contenido
        modalConfig.modal?.addEventListener('click', (e) => {
            if (e.target === modalConfig.modal) {
                modalConfig.modal.classList.remove('active');
            }
        });
    }

    // Configurar todos los modales
    Object.values(modals).forEach(setupModal);

    // Configurar botones de vista de venta individualmente
    if (modals.sale.btns) {
        modals.sale.btns.forEach(btn => {
            btn.addEventListener('click', () => {
                modals.sale.modal.classList.add('active');
            });
        });
    }

    // Gráficos
    const salesCtx = document.getElementById('sales-chart').getContext('2d');
    const topProductsCtx = document.getElementById('top-products-chart').getContext('2d');
    const reportCtx = document.getElementById('report-main-chart').getContext('2d');

    // Datos de ejemplo para los gráficos
    const salesChart = new Chart(salesCtx, {
        type: 'line',
        data: {
            labels: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun'],
            datasets: [{
                label: 'Ventas Mensuales',
                data: [12000, 19000, 15000, 18000, 22000, 25000],
                backgroundColor: 'rgba(108, 92, 231, 0.2)',
                borderColor: 'rgba(108, 92, 231, 1)',
                borderWidth: 2,
                tension: 0.4,
                fill: true
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                }
            },
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });

    const topProductsChart = new Chart(topProductsCtx, {
        type: 'doughnut',
        data: {
            labels: ['iPhone 13', 'Galaxy S22', 'Redmi Note 11', 'AirPods Pro', 'Cargadores'],
            datasets: [{
                data: [35, 28, 20, 12, 5],
                backgroundColor: [
                    '#6c5ce7',
                    '#00b894',
                    '#0984e3',
                    '#fdcb6e',
                    '#d63031'
                ],
                borderWidth: 0
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'right',
                }
            }
        }
    });

    const reportChart = new Chart(reportCtx, {
        type: 'bar',
        data: {
            labels: ['Sem 1', 'Sem 2', 'Sem 3', 'Sem 4', 'Sem 5'],
            datasets: [{
                label: 'Ventas por Semana',
                data: [5000, 6000, 5500, 7000, 8000],
                backgroundColor: 'rgba(108, 92, 231, 0.7)',
                borderColor: 'rgba(108, 92, 231, 1)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                }
            },
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });

    // Filtros de reportes
    const reportType = document.getElementById('report-type');
    const reportPeriod = document.getElementById('report-period');
    const generateReportBtn = document.getElementById('generate-report');

    if (generateReportBtn) {
        generateReportBtn.addEventListener('click', function () {
            // Aquí iría la lógica para generar el reporte según los filtros
            alert(`Generando reporte de ${reportType.value} para el período ${reportPeriod.value}`);
        });
    }

    // Manejo de formularios (simulación)
    const forms = {
        product: document.getElementById('product-form'),
        accessory: document.getElementById('accessory-form'),
        user: document.getElementById('user-form')
    };

    Object.entries(forms).forEach(([key, form]) => {
        if (form) {
            form.addEventListener('submit', function (e) {
                e.preventDefault();
                alert(`Formulario de ${key} enviado con éxito`);
                modals[key].modal.classList.remove('active');
                form.reset();
            });
        }
    });

    // Responsive: Menú para móviles



    // Responsive: Menú para móviles
    const mobileMenuBtn = document.createElement('button');
    mobileMenuBtn.innerHTML = '<i class="fas fa-bars"></i>';
    mobileMenuBtn.classList.add('mobile-menu-btn');
    document.querySelector('.admin-header').prepend(mobileMenuBtn);

    // Variable para rastrear el estado del menú
    let isMenuOpen = false;
    const sidebar = document.querySelector('.sidebar');

    // Función para alternar el menú
    function toggleMenu() {
        isMenuOpen = !isMenuOpen;

        if (isMenuOpen) {
            sidebar.classList.add('active');
            mobileMenuBtn.innerHTML = '<i class="fas fa-times"></i>';
            document.body.style.overflow = 'hidden'; // Evita el scroll cuando el menú está abierto
        } else {
            sidebar.classList.remove('active');
            mobileMenuBtn.innerHTML = '<i class="fas fa-bars"></i>';
            document.body.style.overflow = ''; // Restaura el scroll
        }
    }

    // Evento para el botón de menú
    mobileMenuBtn.addEventListener('click', function (e) {
        e.stopPropagation(); // Evita que el evento se propague
        toggleMenu();
    });

    // Cerrar menú al hacer clic en un ítem del menú (solo en móviles)
    const navLinks = document.querySelectorAll('.admin-nav li');
    navLinks.forEach(link => {
        link.addEventListener('click', function () {
            if (window.innerWidth <= 992) {
                toggleMenu();
            }
        });
    });

    // Cerrar menú al hacer clic fuera de él
    document.addEventListener('click', function (e) {
        if (isMenuOpen && !sidebar.contains(e.target) && e.target !== mobileMenuBtn && !mobileMenuBtn.contains(e.target)) {
            toggleMenu();
        }
    });

    // Prevenir que el clic en el menú lo cierre
    sidebar.addEventListener('click', function (e) {
        e.stopPropagation();
    });
});


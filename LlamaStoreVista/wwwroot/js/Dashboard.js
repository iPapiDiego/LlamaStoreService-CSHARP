
// Sample charts
document.addEventListener('DOMContentLoaded', function () {
    // Sales Chart
    const salesCtx = document.getElementById('salesChart').getContext('2d');
    const salesChart = new Chart(salesCtx, {
        type: 'line',
        data: {
            labels: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun'],
            datasets: [{
                label: 'Ventas 2023',
                data: [1200, 1900, 1500, 2000, 2500, 2200],
                backgroundColor: 'rgba(115, 103, 240, 0.2)',
                borderColor: 'rgba(115, 103, 240, 1)',
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
});







window.drawChart = (canvas, labels, data) => {
    new Chart(canvas, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Prescriptions',
                data: data,
                backgroundColor: 'rgba(34, 197, 94, 0.4)', // 💚 vert transparent
                //borderColor: 'rgb(34, 197, 94)',
                //borderWidth: 1,
                //borderRadius: 6,

                // 🔑 AJOUTS ICI
                barPercentage: 1.0,        // prend toute la largeur possible
                categoryPercentage: 1.0   // chaque mois occupe tout l’espace
            }]
        },
        options: {
            responsive: true,             // ⬅️ TRÈS IMPORTANT (remplit le container)
            maintainAspectRatio: false,

            layout: {
                padding: {
                    left: 10,
                    right: 10
                }
            },

            plugins: {
                legend: {
                    labels: {
                        color: '#6b7280'
                    }
                }
            },
            scales: {
                x: {
                    offset: false,        // ⬅️ SUPPRIME l’espace avant/après
                    ticks: {
                        color: '#6b7280',
                        font: { size: 10 },
                        maxRotation: 0
                    },
                    grid: { display: false }
                },
                y: {
                    beginAtZero: true,
                    ticks: { color: '#6b7280' },
                    grid: {
                        color: 'rgba(0,0,0,0.05)'
                    }
                }
            }
        }
    });
};




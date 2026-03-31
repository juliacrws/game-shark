function renderCharts(categoriaData, plataformaData) {
    // Paleta oficial GameShark (Cyberpunk Neon)
    const neonColors = {
        cyan: '#00f3ff',
        purple: '#bc13fe',
        green: '#39ff14',
        pink: '#ff0055',
        orange: '#ff9d00'
    };

    const bgColors = [
        'rgba(0, 243, 255, 0.6)', // Cyan
        'rgba(188, 19, 254, 0.6)', // Purple
        'rgba(57, 255, 20, 0.6)',  // Green
        'rgba(255, 0, 85, 0.6)',   // Pink
        'rgba(255, 157, 0, 0.6)'   // Orange
    ];

    const borderColors = [
        neonColors.cyan,
        neonColors.purple,
        neonColors.green,
        neonColors.pink,
        neonColors.orange
    ];

    // Configuração Comum para Fontes
    const commonOptions = {
        plugins: {
            legend: {
                labels: {
                    color: '#e0e6ed',
                    font: { family: 'Orbitron', size: 11 }
                }
            }
        }
    };

    // --- Gráfico de Categorias (Pie) ---
    const ctxCat = document.getElementById('chartCategorias').getContext('2d');
    new Chart(ctxCat, {
        type: 'doughnut', // Doughnut fica mais moderno que o Pie comum
        data: {
            labels: categoriaData.map(c => c.label),
            datasets: [{
                data: categoriaData.map(c => c.count),
                backgroundColor: bgColors,
                borderColor: borderColors,
                borderWidth: 2,
                hoverOffset: 15
            }]
        },
        options: {
            ...commonOptions,
            cutout: '70%' // Deixa o gráfico mais fino/elegante
        }
    });

    // --- Gráfico de Plataformas (Bar) ---
    const ctxPlat = document.getElementById('chartPlataformas').getContext('2d');
    new Chart(ctxPlat, {
        type: 'bar',
        data: {
            labels: plataformaData.map(p => p.label),
            datasets: [{
                label: 'Inventário por Plataforma',
                data: plataformaData.map(p => p.count),
                backgroundColor: 'rgba(0, 243, 255, 0.2)',
                borderColor: neonColors.cyan,
                borderWidth: 2,
                borderRadius: 5,
                hoverBackgroundColor: neonColors.cyan
            }]
        },
        options: {
            ...commonOptions,
            scales: {
                y: {
                    beginAtZero: true,
                    grid: { color: 'rgba(255, 255, 255, 0.1)' },
                    ticks: { color: '#888', font: { family: 'Rajdhani' } }
                },
                x: {
                    grid: { display: false },
                    ticks: { color: '#e0e6ed', font: { family: 'Rajdhani', weight: 'bold' } }
                }
            },
            plugins: {
                legend: { display: false } // Esconde a legenda no gráfico de barras
            }
        }
    });
}
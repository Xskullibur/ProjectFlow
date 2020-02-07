function loadDoughnutChart(canvasID, data1, data2) {

    var ctx = document.getElementById(canvasID).getContext('2d')
    var chart = new Chart(ctx, {
        type: 'doughnut',
        data: {
            datasets: [{
                data: [data1, data2],
                backgroundColor: ['Green'],
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: true,
            tooltips: {
                enabled: false
            },
            legend: {
                onClick: (e) => e.stopPropagation()
            }
        }
    })

}

function loadProgressBar(activeIndex) {
    var progressBar = document.getElementById("progressbar");
    progressBar = new Kodhus.StepProgressBar();
    progressBar.init({ activeIndex })
}
document.addEventListener("DOMContentLoaded", function() {
    const account = JSON.parse(sessionStorage.getItem("account"));
    const sellerId = account.id
    // Call API để lấy thông tin đơn hàng
    fetch(`https://www.handmade.somee.com/api/v1/dashboard/seller/${sellerId}`, {
      method: 'GET',
      headers: {
        'accept': 'application/json'
      }
    })
    .then(response => response.json())
    .then(data => {
      // Đặt tổng số đơn hàng
      document.querySelector('#totalOrder').innerHTML = 'Tổng số đơn hàng: ' +  data.orderTotal || 0;
      document.querySelector('#totalIncome').innerHTML = 'Tổng thu nhập: ' + (data.income || 0) + 'VND';

      return data
    })

    .then(data => {
      console.log(data)
      // Extract data for the charts
      const sortedData = data.orderByDays.sort((a, b) => {
        // Convert date strings into Date objects for comparison
        const dateA = new Date(a.date.split("/").reverse().join("-"));
        const dateB = new Date(b.date.split("/").reverse().join("-"));
        return dateA - dateB; // Sort in ascending order
      });
  
      // Extract sorted data for the charts
      const dates = sortedData.map(entry => entry.date);
      const orderTotals = sortedData.map(entry => entry.orderTotal);
      const incomeTotals = sortedData.map(entry => entry.incomeTotal);
      const orderSuccessTotals = sortedData.map(entry => entry.orderSuccessTotal);
  
      // Create line charts using Chart.js
  
      // Chart for Order Total
      const orderTotalCtx = document.getElementById('orderTotalChart').getContext('2d');
      new Chart(orderTotalCtx, {
        type: 'line',
        data: {
          labels: dates,
          datasets: [{
            label: 'Tổng số đơn hàng',
            data: orderTotals,
            borderColor: 'rgba(75, 192, 192, 1)',
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            borderWidth: 3,
            fill: true
          }]
        },
        options: {
          responsive: true,
          plugins: {
              legend: { 
                  display: true,
                  labels: {
                      font: {
                          size: 14, // Font size for legend
                          weight: 'bold' // Bold text for legend
                      }
                  }
              },
              title: { 
                  display: true, 
                  text: 'Tổng số đơn hàng từng ngày',
                  font: {
                      size: 18, // Font size for title
                      weight: 'bold' // Bold text for title
                  }
              }
          },
          scales: {
              x: { 
                  title: { 
                      display: true, 
                      text: 'Ngày',
                      font: {
                          size: 14, // Font size for x-axis title
                          weight: 'bold' // Bold text for x-axis title
                      }
                  }
              },
              y: { 
                  title: { 
                      display: true, 
                      text: 'Tổng số đơn',
                      font: {
                          size: 14, // Font size for y-axis title
                          weight: 'bold' // Bold text for y-axis title
                      }
                  },
                  ticks: {
                      callback: function(value) {
                          if (Number.isInteger(value)) {
                              return value;
                          }
                      },
                stepSize: 1 // Ensure that the steps are whole numbers
              }
            }
          }
        }
      });
  
      // Chart for Income Total
const incomeTotalCtx = document.getElementById('incomeTotalChart').getContext('2d');
new Chart(incomeTotalCtx, {
    type: 'line',
    data: {
        labels: dates,
        datasets: [{
            label: 'Tổng thu nhập',
            data: incomeTotals,
            borderColor: 'rgba(153, 102, 255, 1)',
            backgroundColor: 'rgba(153, 102, 255, 0.2)',
            borderWidth: 3, // Increased thickness
            fill: true
        }]
    },
    options: {
        responsive: true,
        plugins: {
            legend: { 
                display: true,
                labels: {
                    font: {
                        size: 14, // Font size for legend
                        weight: 'bold' // Bold text for legend
                    }
                }
            },
            title: { 
                display: true, 
                text: 'Doanh thu theo ngày',
                font: {
                    size: 18, // Font size for title
                    weight: 'bold' // Bold text for title
                }
            }
        },
        scales: {
            x: { 
                title: { 
                    display: true, 
                    text: 'Ngày',
                    font: {
                        size: 14, // Font size for x-axis title
                        weight: 'bold' // Bold text for x-axis title
                    }
                }
            },
            y: { 
                title: { 
                    display: true, 
                    text: 'Tổng doanh thu (VND)',
                    font: {
                        size: 14, // Font size for y-axis title
                        weight: 'bold' // Bold text for y-axis title
                    }
                }
            }
        }
    }
});

// Chart for Order Success Total
const orderSuccessTotalCtx = document.getElementById('orderSuccessTotalChart').getContext('2d');
new Chart(orderSuccessTotalCtx, {
    type: 'line',
    data: {
        labels: dates,
        datasets: [{
            label: 'Tổng số đơn thành công',
            data: orderSuccessTotals,
            borderColor: 'rgba(255, 99, 132, 1)',
            backgroundColor: 'rgba(255, 99, 132, 0.2)',
            borderWidth: 3, // Increased thickness
            fill: true
        }]
    },
    options: {
        responsive: true,
        plugins: {
            legend: { 
                display: true,
                labels: {
                    font: {
                        size: 14, // Font size for legend
                        weight: 'bold' // Bold text for legend
                    }
                }
            },
            title: { 
                display: true, 
                text: 'Đơn đặt hàng thành công theo ngày',
                font: {
                    size: 18, // Font size for title
                    weight: 'bold' // Bold text for title
                }
            }
        },
        scales: {
            x: { 
                title: { 
                    display: true, 
                    text: 'Ngày',
                    font: {
                        size: 14, // Font size for x-axis title
                        weight: 'bold' // Bold text for x-axis title
                    }
                }
            },
            y: { 
                title: { 
                    display: true, 
                    text: 'Đơn hàng thành công',
                    font: {
                        size: 14, // Font size for y-axis title
                        weight: 'bold' // Bold text for y-axis title
                    }
                },
                ticks: {
                    callback: function(value) {
                        if (Number.isInteger(value)) {
                            return value;
                        }
                    },
                    stepSize: 1 // Ensure whole number steps
                }
            }
        }
    }
});

  });
});
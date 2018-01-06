define(function(require, exports, module) {
    'use strict';

    var LayoutManager = require('layoutmanager');
    var template = require('text!./template.html');
    var commonFunction = require('commonfunction');
    var eventAggregator = require('eventaggregator');
    var Collection = require('./collection');
    require('highcharts');

    module.exports = LayoutManager.extend({
        el: false,
        template: _.template(template),
        initialize: function() {
            var self = this;
        },
        events: {
            
        },
        afterRender: function() {
            this.renderSPAMBandarLampuung();
            this.renderPalapaRingNetworkTimur();
            this.renderPalapaRingNetworkTengah();
            this.renderPalapaRingNetworkBarat();
            this.renderRiskCapitalProjectTolPandaanMalang();
            this.renderRiskCapitalProjectTolManadoBitung();
        },
        renderSPAMBandarLampuung: function() {
            Highcharts.chart('riskSPAMBandarLampung', {
                title: {
                    text: 'SPAM Bandar Lampung'
                },
                xAxis: {
                    categories: [2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028, 2029, 2030, 2031, 2032]
                },
                plotOptions: {
                    series: {
                        allowPointSelect: true
                    }
                },
                series: [{
                    data: [0.00, 18.36, 39.71, 54.52, 55.30, 54.40, 40.30, 38.83, 36.94, 34.76, 32.45, 30.06, 27.64, 35.36, 23.16, 22.73],
                    name: 'Tahun'
                }]
            });
        },
        renderPalapaRingNetworkTimur: function() {
            Highcharts.chart('riskProjectPalapaRingNetworkTimur', {
                title: {
                    text: 'Palapa Ring Network Timur'
                },
                xAxis: {
                    categories: [2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028, 2029, 2030, 2031, 2032]
                },
                plotOptions: {
                    series: {
                        allowPointSelect: true
                    }
                },
                series: [{
                    data: [63.00, 212.83, 303.24, 284.16, 264.05, 243.13, 221.80, 206.56, 193.47, 181.06, 170.11, 163.84, 161.03, 159.78, 0.00, 0.00],
                    name: 'Tahun'
                }]
            });
        },
        renderPalapaRingNetworkTengah: function() {
            Highcharts.chart('riskProjectPalapaRingNetworkTengah', {
                title: {
                    text: 'Palapa Ring Network Tengah'
                },
                xAxis: {
                    categories: [2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028, 2029, 2030, 2031, 2032]
                },
                plotOptions: {
                    series: {
                        allowPointSelect: true
                    }
                },
                series: [{
                    data: [53.58, 70.62, 66.90, 62.97, 58.85, 54.58, 51.56, 48.68, 46.36, 44.13, 41.76, 42.26, 40.98, 0.00, 0.00, 0.00],
                    name: 'Tahun'
                }]
            });
        },
        renderPalapaRingNetworkBarat: function() {
            Highcharts.chart('riskProjectPalapaRingNetworkBarat', {
                title: {
                    text: 'Palapa Ring Network Barat'
                },
                xAxis: {
                    categories: [2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028, 2029, 2030, 2031, 2032]
                },
                plotOptions: {
                    series: {
                        allowPointSelect: true
                    }
                },
                series: [{
                    data: [49.94, 65.63, 58.67, 54.79, 51.25, 47.38, 45.04, 43.22, 41.51, 39.31, 40.14, 38.66, 0.00, 0.00, 0.00],
                    name: 'Tahun'
                }]
            });
        },
        renderRiskCapitalProjectTolManadoBitung: function () {
            Highcharts.chart('riskProjectTolManadoBitung', {
                title: {
                    text: 'Tol Manado - Bitung'
                },
                xAxis: {
                    categories: [2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028, 2029, 2030, 2031, 2032]
                },
                plotOptions: {
                    series: {
                        allowPointSelect: true
                    }
                },
                series: [{
                    data: [202.14, 122.84, 342.32, 321.84, 313.48, 308.33, 300.14, 298.26, 299.09, 298.26, 283.53, 262.41, 243.91, 222.43, 202.95, 182.51],
                    name: 'Tahun'
                }]
            });
        },
        renderRiskCapitalProjectTolPandaanMalang: function() {
            Highcharts.chart('riskProjectTolPandaanMalang', {
                title: {
                    text: 'Tol Pandaan - Malang'
                },
                xAxis: {
                    categories: [2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028, 2029, 2030, 2031, 2032]
                },
                plotOptions: {
                    series: {
                        allowPointSelect: true
                    }
                },
                series: [{
                    data: [323.88, 166.77, 523.57, 482.51, 467.27, 443.77, 420.77, 396.81, 374.31, 350.15, 327.94, 303.50, 281.68, 256.88, 234.07, 210.31],
                    name: 'Tahun'
                }]
            });
        }
    });
});
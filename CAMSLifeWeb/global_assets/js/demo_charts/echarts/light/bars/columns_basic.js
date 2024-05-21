/* ------------------------------------------------------------------------------
 *
 *  # Echarts - Basic column example
 *
 *  Demo JS code for basic column chart [light theme]
 *
 * ---------------------------------------------------------------------------- */


// Setup module
// ------------------------------

var EchartsColumnsBasicLight = function() {


    //
    // Setup module components
    //

    // Basic column chart
    var _columnsBasicLightExample = function() {
        if (typeof echarts == 'undefined') {
            console.warn('Warning - echarts.min.js is not loaded.');
            return;
        }

        // Define element
        var columns_basic_element = document.getElementById('columns_basic');


        //
        // Charts configuration
        //

        if (columns_basic_element) {

            // Initialize chart
            var columns_basic = echarts.init(columns_basic_element);


            //
            // Chart config
            //

            // Options
            columns_basic.setOption({

                // Define colors
              
            });
        }


        //
        // Resize charts
        //

        // Resize function
        var triggerChartResize = function () {
            console.log('resize'); 
            columns_basic_element && columns_basic.resize();
        };

        // On sidebar width change
        var sidebarToggle = document.querySelectorAll('.sidebar-control');
        if (sidebarToggle) {
            sidebarToggle.forEach(function(togglers) {
                togglers.addEventListener('click', triggerChartResize);
            });
        }

        // On window resize
        var resizeCharts;
        window.addEventListener('resize', function() {
            clearTimeout(resizeCharts);
            resizeCharts = setTimeout(function () {
                triggerChartResize();
            }, 200);
        });
    };


    //
    // Return objects assigned to module
    //

    return {
        init: function() {
            _columnsBasicLightExample();
            triggerChartResize();
        }
    }
}();


// Initialize module
// ------------------------------

document.addEventListener('DOMContentLoaded', function() {
    EchartsColumnsBasicLight.init();
});

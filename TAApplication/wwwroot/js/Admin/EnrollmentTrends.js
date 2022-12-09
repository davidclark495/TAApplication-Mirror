// /*
//     Author: Robert Davidson
//     Partner: David Clark
//     Date: 12/06/2022
//     Course: CS 4540, University of Utah, School of Computing
//     Copyright: CS 4540, David Clark and Robert Davidson - This work may not be copied for use in Academic Coursework.
//	
//     I, David Clark, certify that I wrote this code from scratch and did not copy it in part or whole from 
//     another source.  Any references used in the completion of the assignment are cited in my README file.
//
//     I, Robert Davidson, certify that I wrote this code from scratch and did not copy it in part or whole from
//     another source. Any references used in the completion of the assignment are cited in my README file.
//
//     File Contents
//
//         Sets up the High Chart and handles page activity
// */

// Gets form data
document.getElementById('enrollment-data-form').onsubmit = function() {
    loadFormData();
    return false;
};

// Loads form data
function loadFormData()
{
    set_button_loading();
    
    let start_date = document.getElementById('start-date').value;
    let end_date = document.getElementById('end-date').value;
    let course = document.getElementById('course-identifier').value.split(' ');
    let course_dept = course[0];
    let course_no = course[1];
    
    $.get(
        {
            url: "/Admin/GetEnrollmentData/",
            data: { 
                start: start_date, 
                end: end_date, 
                courseDept: course_dept, 
                courseNum: course_no 
            },
        }
    ).done(function (data) {
        set_button_finished();
        chartEnrollmentData(data);
    });
}

// Plots passed data onto Highchart(s)
function chartEnrollmentData(data) {
    let date_enrollment_data = data.map(er => [Date.parse(er['date']), er['enrollment']]);
    let course_name_short = data[0]["course"]["department"] + " " + data[0]["course"]["number"];
    $("#chart").highcharts().addSeries(
    {
        name: course_name_short,
        data: date_enrollment_data 
    });
    
    let last_course_enrollment_data = [course_name_short, data[data.length-1]['enrollment']];
    $("#bar-chart").highcharts().addSeries({
        name: last_course_enrollment_data[0],
        data: [last_course_enrollment_data[1]]
    });

}

set_button_finished();

// Initiates highchart(s)
$(document).ready( function() {
    // Line Chart
    $("#chart").highcharts({
        type: 'line',
        title: {text: 'Enrollments Over Time'},
        subtitle: {text: ''},
        yAxis: {title: {text: 'Students'}},
        xAxis: {
            title: {text: 'Dates'}, 
            type: 'datetime',
            labels: {
                step: 7,
                format: '{value:%b. %d}' 
            }
        },
        legend: {layout: 'vertical', align: 'left', verticalAlign: 'middle'},
        plotOptions: {
            series: {
                label: {connectorAllowed: false},
                pointStart: 0
            }
        }
    });
    
    // Bar Chart
    $("#bar-chart").highcharts({
        chart: {
            type: 'bar'
        },
        title: {text: 'Current Enrollments'},
        subtitle: {text: ''},
        yAxis: {title: {text: 'Students'}},
        xAxis: {
            visible: false
        },
        legend: {layout: 'vertical', align: 'left', verticalAlign: 'middle'},
        plotOptions: {
            bar: {
                dataAsColumns: true
            }
        }
    });
});

// Shows spinner on submit button
function set_button_loading(){
    $("#get-data-button-loading").removeClass("d-none");    // similar to 'show'
    $("#get-data-button-default").addClass("d-none");       // similar to 'hide'
}

// Hides spinner on submit button when data is loaded
function set_button_finished(){
    $("#get-data-button-loading").addClass("d-none");       // similar to 'hide'
    $("#get-data-button-default").removeClass("d-none");    // similar to 'show'
}
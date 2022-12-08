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

// ###################################################################################################################
// //$(document).ready(function () {
// //    $('#roleManager').DataTable();
// //});
//
// //function setRole(unid,role) {
// //    $.post(
// //        {
// //            url: "/Admin/SetRole",
// //            data: {
// //                "unid": unid,
// //                "role": role
// //            }
// //        })
// //        .done(function (data){
// //        console.log("Sample of Data:", data);
// //    });
// //}
// ###################################################################################################################

document.getElementById('enrollment-data-form').onsubmit = function() {
    loadFormData();
    return false;
};


function loadFormData()
{
    $("#get-data-button-loading").show();
    $("#get-data-button-default").hide();
    
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
        $("#get-data-button-loading").hide();
        $("#get-data-button-default").show();
        chartEnrollmentData(data);
    });
}

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

$("#get-data-button-loading").hide();
$("#get-data-button-default").show();

$(document).ready( function() {
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
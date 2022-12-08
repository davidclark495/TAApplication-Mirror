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
    let start_date = document.getElementById('start-date').value;
    let end_date = document.getElementById('end-date').value;
    let course = document.getElementById('course-identifier').value.split(' ');
    getEnrollmentData(start_date, end_date, course[0], course[1]);
    return false;
};

function getEnrollmentData(start_date, end_date, course_dept, course_no)
{
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
        console.log("Sample of Data: ", data);
        chartEnrollmentData(data);
    });
}

function chartEnrollmentData(data) {
    
}
/*
    Author: Robert Davidson
    Partner: David Clark
    Date: 09/23/2022
    Course: CS 4540, University of Utah, School of Computing
    Copyright: CS 4540, David Clark and Robert Davidson - This work may not be copied for use in Academic Coursework.
	
    I, David Clark, certify that I wrote this code from scratch and did not copy it in part or whole from 
    another source.  Any references used in the completion of the assignment are cited in my README file.

    I, Robert Davidson, certify that I wrote this code from scratch and did not copy it in part or whole from
    another source. Any references used in the completion of the assignment are cited in my README file.

    File Contents

        Sets up the data table and sends role information to backend with AJAX and JSON
*/

$(document).ready(function () {
    $('#roleManager').DataTable();
});

function setRole(unid,role) {
    $.post(
        {
            url: "/Admin/SetRole",
            data: {
                "unid": unid,
                "role": role
            }
        })
        .done(function (data){
        console.log("Sample of Data:", data);
    });
}

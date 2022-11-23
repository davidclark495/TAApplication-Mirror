/*
 Author:    Robert Davidson
 Partner:   David Clark
 Date:      11/23/2022
 Course:    CS 4540, University of Utah, School of Computing
 Copyright: CS 4540, David Clark and Robert Davidson - This work may not be copied for use in Academic Coursework.


 I, David Clark, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of the assignment are cited in my README file.
 I, Robert Davidson, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of the assignment are cited in my README file.


 File Contents
	This is the C# model for Availability Time Slots
 */

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TAApplication.Areas.Identity.Data;

namespace TAApplication.Models
{
    public class Slot
    {
        [Required]
        public int ID { get; set; }

        // Bool for Availibilty
        [Required]
        public bool IsOpen { get; set; }

        // Slot section identity
        //      MONDAY:     0 -> 8am, 1 -> 8:15am, ..., 47 -> 7:45pm
        //      TUESDAY:    48 -> 8am, 49 -> 8:15am, ..., 95 -> 7:45pm
        //      WEDNESDAY:  96 -> 8am, 97 -> 8:15am, ..., 143 -> 7:45pm
        //      THURSDAY:   144 -> 8am, 145 -> 8:15am, ..., 191 -> 7:45pm
        //      FRIDAY:     192 -> 8am, 193 -> 8:15am, ..., 239 -> 7:45pm
        [Required]
        [Range(0,239)]
        public int SlotNumber { get; set; }

        // Navigation Properties
        [Required]
        [ForeignKey("TAUser")]
        public string TAUserId { get; set; }

        // the TAUser who owns this time slot
        [Required]
        public TAUser TAUser { get; set; } = null!;
    }
}

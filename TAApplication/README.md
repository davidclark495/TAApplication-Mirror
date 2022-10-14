```
Author:     Robert Davidson
Partner:    David Clark
Date:       23-Sep-2022
Course:     CS 4540, University of Utah, School of Computing
GitHub ID:  rj-davidson
Partner GitHub ID: davidclark495
Repo:       https://github.com/uofu-cs4540-fall2022/taapplication-bazooka-pompom.git
EC2 name (Robert): 
EC2 name (David):
Commit Tag: HW5
Project:    TA Application
Copyright:  CS 4540, David Clark and Robert Davidson  - This work may not be copied for use in Academic Coursework.
```
# Overview of the TA Application Functionality
    
    TAApplication has sign-in and register features, with users separated
into 3 roles, Spplicants, Professors, and Admins. Each user has one role, and will see
different pages/links based on that role. Both frontend and backend are independently secured
and validated.

    It allows Applicants to submit applications, which include academic and other data. Some fields are optional.
Applicants can upload resumes (.pdf) and images (.png, .jpg, .gif).

# Comments to Evaluators:

Apparently we are a little slower than we thought, this took us a while.

The Admin is not permmitted to edit Applications.

All commits were from one github account as we did all work together on zoom using remote control to switch paired programming roles. Both of us spent equal time driving and navigating.

# file / Image Uploads

We implemented file-uploads. Users can upload resumes and images as described in the specifications.

# Assignment Specific Topics

    The Modification-Tracking code allows users to see how old data is and how recently it hae been modified. 
It establishes who originally created the data and who has touched it recently. For any class, it will  
allow users to track data as it changes over time. 

    For our TAApplication, tracking this data for Applications is very useful. The "ModifiedTime" in particular
allows Professors and Admins to see if an applicant has recently changed their application. An applicant may be  
expected to revise and improve their application as they acquire more experience and take more classes, so having an
in-site way of representing that is essential.

    The code exists in two parts: first, in a Model class with fields inherited by other data classes,
and second, in updates to certain ApplicationDbContext methods. The second part actually overrides the SaveChanges() method,
slipping in a "AddTimestamps()" call before actually saving changes. The new AddTimestamps() function iterates through
all the data objects that have been touched and checks if any inherit the ModificationTracking model class; if any do,
their fields are updated, and the database is allowed to resume as normal.

# Consulted Peers:

Once again we have disappointed by not consuling other classmates. We didn't have much to show design-wise, as we spent most 
of our time fixing bugs and meeting basic specifications.

# Peers Helped:

None, see above.

# Acknowledgements:  

Lecture Image (Kevin Dooley) - https://wordpress.org/openverse/image/29648f7a-c935-4e1b-ad61-10b97e6ddc80/
Books Image - https://pxhere.com/en/photo/1575601
Trophy Image - https://www.publicdomainpictures.net/en/view-image.php?image=163466&picture=trophy

# References:

    - U of U Admissions page - https://admissions.utah.edu/apply/
    - W3 Schools - https://w3schools.com
    - Bootstrap - https://getbootstrap.com
    - Bootswatch - https://bootswatch.coms

# Time Expenditures:

   1. Assignment One: Predicted Hours: 10 Actual Hours: 10
   2. Assignment Two: Predicted Hours: 10 Actual Hours: 8.5
   
   4. Assignment Four: Predicted Hours: 10 Actual Hours: 18
   5. Assignment Five: Predicted Hours: 15 Actual Hours: 20

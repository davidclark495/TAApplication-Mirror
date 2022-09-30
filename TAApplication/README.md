```
Author:     Robert Davidson
Partner:    David Clark
Date:       23-Sep-2022
Course:     CS 4540, University of Utah, School of Computing
GitHub ID:  rj-davidson
Partner GitHub ID: davidclark495
Repo:       https://github.com/uofu-cs4540-fall2022/ps1---applicantlist-flagrantstork
EC2 name (Robert): 
EC2 name (David): 
Commit Tag: HW4
Project:    TA Application
Copyright:  CS 4540, David Clark and Robert Davidson  - This work may not be copied for use in Academic Coursework.
```
# Overview of the TA Application Functionality

TAApplication has sign-in and register features, with users separated
into 3 roles, Spplicants, Professors, and Admins. Each user has one role, and will see
different pages/links based on that role. Both frontend and backend are independently secured
and validated.

# Comments to Evaluators:

We had some techniacal issues, and it took us a while to get going. The assignment took more out of our schedules
than we had prepared for.

# Assignment Specific Topics

Our site handles authentication and authorization as expected. Authentication is well handled by the default ASP Core system.
We appreciate the security that comes with not writing our own password managing scheme. Authorization is mostly done 
with data annotations that restrict access to pages based on roles, but we also wrote code to limit access to the 
ApplicationDetails page to a specific user, per the specifications. We looked into policy-based authorization just enough
to know that it wasn't what we needed. It's clear that we've only scratched the surface.

The site does what is expected, and not a lot more. Our registration page accepts email addresses with and without the
"umail." specifier, but it cuts the snippet out before adding it to the new user. New users must have unique u-of-u email
addresses, and thus unique unids.

-na-

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
   3. Assignment Four: Predicted Hours: 10 Actual Hours: 17

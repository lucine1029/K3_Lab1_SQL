# K3_Lab1_SQL
A good practice of using ADO.NET to access and manipulate database with sql.

# About the task
In this lab, you will demonstrate that you can connect to and call a database using SQL.

# What you will do
**Create a database**
Create a simple database that stores the following. The database does not need to have an advanced architecture.
- [ ]  Pupils
- [ ] Classes that students can belong to
- [ ] Courses
- [ ] Grades received by a particular student in a particular course (store as numbers)
- [ ] Personnel who may belong to different categories (e.g. teacher, administrator, principal)

**Create the basis of application**
- [ ] Create a new console program in C#
- [ ] Create a simple navigation in the program that allows the user to choose between the functions below. Here, a simple case sentence is sufficient, and the user enters a number to navigate to a certain function.
- [ ] When a function has been executed, the user must be able to click enter and return to the main menu again.

**Create the functionality**
You must create the following functions that the user can navigate to in the application. All these functions should be solved through pure SQL 
1. **Get all students**
     The user can choose whether they want to see the students sorted by first or last name and whether it should be sorted in ascending or descending order.
    
2. **Get all students in a certain class**
     The user must first see a list of all classes that exist, then the user can select one of the classes and then all the students in that class will be printed.
     Extra challenge: let the user also choose the sorting of the students as in the point above.
3. **Add new staff**
     The user must be able to enter information about a new employee and that data is then saved in the database.
4. **Get staff**
     The user can choose whether he wants to see all employees, or only within one of the categories, such as teachers.
5. **Get all grades set in the last month*
     Here, the user can see a list of all the grades set in the last month, where the student's name, the name of the course and the grade appear.
6. **Average grade per course**
     Get a list of all courses and the average grade that students received in that course, as well as the highest and lowest grade that someone received in the course.
     Here the user can see a list of all courses in the database, the average grade and the highest and lowest grade for each course.
     Tip: It can be difficult to do this with grades in the form of letters so you can choose to store the grades as numbers instead.
7. **Add new students**
     The user gets the opportunity to enter information about a new student and that data is then saved in the database.
👉 **Extra Challenges**
- Check that the social security numbers are valid through SQL.
- Build a view to retrieve grades set in the last month.
- Build another function for the user where it is possible to obtain the average grade based partly on gender and partly on age group/year group, in terms of the average for all courses they have taken.

# CodeLab 
--------------------------------------------------------
--------------------SOLUTION----------------------------
--------------------------------------------------------
## Solution 

## A. Preparation part:

1. First I grasped what is required and what I have.
2. To visualize the case, I draw out the structure of the data (see Chart in ExpFile.pdf)
3. Then I created a table to see all the values per insurances in parent-child relation. (see Table1 in ExpFile.pdf)
4. I prepared another table that depicts cumulative values of insurance groups in parent-child relation per each depth. (see Table2 in ExpFile.pdf)
5. And later I decided to loop in all object values in the "insuranceList" and assign them to the array "insuranceArr" cumulatively.
6. After getting the array of values, sorting and taking all the elements would retun the required result and solve the problem.
6. So I prepared another table to find out and see what the the values I should get each time after I run my code. (see Table 3 in ExpFile.pdf)
7. By this way I would predict what I should get after I run my code after each test.

## B. Coding part:

1. First I created Controller folder to place my "InsuranceController" file to create my endpoint.
2. In controller class I created a constructer and a private field to be able to reach the repository file and GetAll() method in it to be able to get the data and assign it to "insuranceList"
3. I created an HttpGet action method that takes two parameters which are "maxCount" and "maxDepth".
4. To specify the endpoint I used decoration of [HttpGet("top/{maxCount}/{maxDepth}")] and pass the parameters as the path parameters.
5. I craeted a variable "insuranceList" to get all the List of insurances from db.
6. And cretaed an array that I will use to store all the values that i get from the list in loops.
7. I declared a variable "i" to handle the index issue of array in loops.
8. In my solution there are 4 depths in accordance with case which is enough to solve.
9. I use three different properties of insurance object. Those are "Children", "Value" and "Depth". 
10. Children and Value properties were alredy embedded but I added another property which is called "Depth" and assigned it to 0 to manage loop issues.
11. In each loop, I visit all the elements. In each, first I increment Depth value by 1, then I get the value and add it to the variable "itemSum" and check if the item has "Children" and if it reaches the "maxDepth" limit.
12. If there is child or it has not reach the maxDepth limit, it starts looping in sub "Children"  which is the other object that has parent-child relationship. 
13. This repeats itself in loops until the "Depth" reaches "maxDepth" value or there is no "Children". (If length or count of Children array equals to zero, it means that there is no child.) 
14. At this point in each loop, "itemSum" is added the the "insuranceArr" array with the index of "i" and increment i with 1.
15. With "continue" in if statement I stop the current iteration and pass the next one.
16. At the end of the loop, the array  "insuranceArr" are fully assigned.
17. To get the required result, I use linq methods. Order the array from big to small, take only the required number of elements and assignthe shaped/filtered array to my finalArray which I will retun.

## C.Testing part:
1. I create a data table to use in test to check if the code runs as expected. (see Table 3 in ExpFile.pdf)
2. I can check and get the the result by using this table.

--------------------------------------------------------
--------------------SOLUTION ENDS-----------------------
--------------------------------------------------------

## Information
Data-models used in the service is of type Insurance.

Each model has these properties:
- InsuranceId is the unique id of the insurance.
- Name is the name of the insurance.
- Value is the value of the insurance.
- ParentId is the parent insurance id to the insurance.
- Parent is the parent insurance to the insurance.
- Children contain all sub-insurances.

```src\api``` project - this is where you add your implementation.

```src\tests``` project - this is where the requirements are implemented as a test to verify the implementation.

## Scenario
We need an endpoint that can return top combined values in insurances with depth restraints.

### Requirements
 - Combine value in parent insurance with sub-insurances.
 - We need to be able to set the maximum amount of returned values. Utilize the maxCount parameter.
 - We need to be able to limit the depth of sub-insurances calulated. Utilize the maxDepth parameter.

## Hints
Seed data for the test can be found in the Seed method of the the ```Tests.cs``` class.
If you want to change it, feel free to do so.
However, know that the current theory is based on initial seed data.
Changing test seed data could potentially break the theory.
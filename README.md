# CodeLab 

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

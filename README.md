# Main goal is creation of simple neural network for number prediction.
Predictions will be processed on image, which displays the set of number from 0 to 9.
All weights was calculated manually.
### Description of work 
The main form of app contain an image with set of numbers. 
You can choose the number, which needed to recognize.
Also on the form is existed 4 buttons:
- Generate [Generate random weights]
- Load [For loading custom weights in format .CSV]
- Save 
- Compute [Compute output signals]
* Correct weights is located at working directory of project(See Debug or Release directory).
### This application is an implementation of Rosenblatt’s perceptron.
Input signals are multiplied on weights of each number and generates output signals. The highest value of output is predicted number.
NB: The activation function was scipped.
![Simple perceptron](https://user-images.githubusercontent.com/37273805/141051315-360cba5a-1745-4d8c-8c1c-d5cf9556303c.png)

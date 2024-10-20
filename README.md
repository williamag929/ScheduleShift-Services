# C6-12-net Shift Work Schedule

***About***
    This application allows you to schedule staff in the daily shift work, it also allows workers to record their time of entry and exit, the system calculates the timecard of the day.

	*Url app:[ Follow this link](https://main.d23hrr0t3ac536.amplifyapp.com/)

    usuario: demo, clave: demo

***Technology**

    *FrontEnd: Angular 17.0*
    *Backend: NetCore 6.0*
    *Database: Sql Server*
    *schedule: full calendar 6.0*
    *Containers: Docker*
 

***Team Members**

    *William Aguirre: FullStack*
    *Maxi Palermo: FullStack*
    *Jose Moncada: FullStack*


***How to deploy**

    > we use docker for easy deploy just run the follow command in Terminal
    first you have to install docker from this site https://www.docker.com/

```
#backend run


 dotnet build
 
 docker build -t shiftworkbackend .
 docker run -d -p 8080:80 --name shiftworkbackend waguirre82/shiftworkbackend:latest

 https://localhost:7054/Swagger/index.html

#frontend run

npm install
npm ci
npm start

docker build -t shiftworkfrontend .

docker run -d -it -p 80:80/tcp --name shift-workfrontend shiftworkfrontend:latest



```


# database update #

1. on visual studio open -Package Manager Console

2. PM>   Add-Migration AddTAsk{TimeStart}

PM> Update-Database



# Console #

1. dotnet ef database update



# C6-12-net Shift Work Schedule

***About***
    This application allows you to schedule staff in the daily shift work, it also allows workers to record their time of entry and exit, the system calculates the timecard of the day.

	*Url app:[ Follow this link](https://main.d23hrr0t3ac536.amplifyapp.com/)

    usuario: demo, clave: demo

***Technology**

    *FrontEnd: Angular 14.0*
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
 docker run -d -p 8080:80 --name shiftworkbackend shiftworkbackend:latest

#frontend run

npm install
npm ci
npm start

docker build -t shiftworkfrontend .

docker run -d -it -p 80:80/tcp --name shift-workfrontend shiftworkfrontend:latest

```
Set up SonarQube static analysis:

1. In Powershell start SonarQube image created from yml file
docker-compose -f .\etc\docker-compose.sonarqube.yml up -d

 To stop SonarQube instance use the following command:
docker-compose -f .\etc\docker-compose.sonarqube.yml down

Note: The local SonarQube instance uses in-memory data storage. 
Any changes made will be lost on restart

2. Create SonarQube project to analyze
Navigate to http://localhost:9000/dashboard and use admin/admin to login.
You can use "pushshift-api" for project key or display name.
Generate a token by using any keyword

Copy etc\tmpl\sonar-config.xml file to etc\conf
Leave all the configurations as is, and only replace "YOUR_LOGIN_TOKEN" placeholder
with an actual token that you have just generated

3. Install SonarScanner for .NET
dotnet tool install --global dotnet-sonarscanner

4. Execute nant sonar command and navigate to 

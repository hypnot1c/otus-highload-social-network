COPY 
  "User" ("PublicId", "Firstname", "Secondname", "BirthDate", "Biography", "City") 
FROM 
  '/docker-entrypoint-initdb.d/user_data.copy'
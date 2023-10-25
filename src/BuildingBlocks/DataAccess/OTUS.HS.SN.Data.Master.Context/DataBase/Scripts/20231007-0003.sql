COPY 
  "User" ("PublicId", "Firstname", "Secondname", "BirthDate", "Biography", "City", "PasswordHash") 
FROM 
  '/docker-entrypoint-initdb.d/user_data.copy'
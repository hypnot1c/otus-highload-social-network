#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
	CREATE DATABASE otus_social_network;
	CREATE USER web_api WITH ENCRYPTED PASSWORD 'password';

	GRANT ALL PRIVILEGES ON DATABASE otus_social_network TO web_api;

	GRANT CONNECT ON DATABASE otus_social_network TO web_api;

	GRANT USAGE ON SCHEMA public TO web_api;

	GRANT pg_read_server_files TO web_api;

	GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO web_api;
EOSQL

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "otus_social_network" <<-EOSQL
	GRANT ALL ON SCHEMA public TO web_api;
EOSQL

pgbench -i -U web_api otus_social_network

psql -v ON_ERROR_STOP=1 --username "web_api" --dbname "otus_social_network" <<-EOSQL
	CREATE TABLE "User" (
		"Id"            SERIAL NOT NULL,
		"PublicId"      uuid NOT NULL,
		"Firstname"     varchar(100) NOT NULL,
		"Secondname"    varchar(200) NOT NULL,
		"BirthDate"     date,
		"Biography"     varchar(5000),
		"City"          varchar(100),
		"PasswordHash"  varchar(300)
	);

	CREATE INDEX "User_Firstname_Secondname_Lower" ON "User" (Lower("Firstname") varchar_pattern_ops, Lower("Secondname") varchar_pattern_ops);
EOSQL

psql -v ON_ERROR_STOP=1 --username "web_api" --dbname "otus_social_network" <<-EOSQL
	COPY "User" ("PublicId", "Firstname", "Secondname", "BirthDate", "Biography", "City", "PasswordHash") FROM '/docker-entrypoint-initdb.d/user_data.copy'
EOSQL

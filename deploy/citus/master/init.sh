#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
	CREATE DATABASE otus_social_network;
	CREATE USER web_api WITH ENCRYPTED PASSWORD 'password';

	GRANT ALL PRIVILEGES ON DATABASE otus_social_network TO web_api;

	GRANT CONNECT ON DATABASE otus_social_network TO web_api;

	GRANT USAGE ON SCHEMA public TO web_api;

	GRANT pg_read_server_files TO web_api;

	ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT ON TABLES TO web_api;
EOSQL

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "otus_social_network" <<-EOSQL
	GRANT ALL ON SCHEMA public TO web_api;
	
	CREATE EXTENSION citus;
EOSQL

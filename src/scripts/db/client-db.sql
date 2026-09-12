CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260911164942_InitialCreate') THEN
    CREATE TABLE "Clientes" (
        "Id" uuid NOT NULL,
        "Password" character varying(500) NOT NULL,
        "Estado" character varying(20) NOT NULL,
        "CreatedAt" timestamp with time zone,
        "CreatedBy" text,
        "LastModified" timestamp with time zone,
        "LastModifiedBy" text,
        "Nombre" character varying(150) NOT NULL,
        "Genero" character varying(20) NOT NULL,
        "Edad" integer NOT NULL,
        "Identificacion" character varying(20) NOT NULL,
        "Direccion" character varying(250) NOT NULL,
        "Telefono" character varying(20) NOT NULL,
        CONSTRAINT "PK_Clientes" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260911164942_InitialCreate') THEN
    CREATE UNIQUE INDEX "IX_Clientes_Identificacion" ON "Clientes" ("Identificacion");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260911164942_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260911164942_InitialCreate', '8.0.11');
    END IF;
END $EF$;
COMMIT;


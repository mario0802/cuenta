CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260911215457_InitialCreate') THEN
    CREATE TABLE "Cuentas" (
        "Id" uuid NOT NULL,
        "ClienteId" uuid NOT NULL,
        "NumeroCuenta" character varying(12) NOT NULL,
        "TipoCuenta" character varying(20) NOT NULL,
        "SaldoInicial" numeric(18,2) NOT NULL,
        "Estado" character varying(20) NOT NULL,
        "CreatedAt" timestamp with time zone,
        "CreatedBy" text,
        "LastModified" timestamp with time zone,
        "LastModifiedBy" text,
        CONSTRAINT "PK_Cuentas" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260911215457_InitialCreate') THEN
    CREATE TABLE "Movimientos" (
        "Id" uuid NOT NULL,
        "CuentaId" uuid NOT NULL,
        "TipoMovimiento" character varying(20) NOT NULL,
        "Fecha" timestamp with time zone NOT NULL,
        "Valor" numeric(18,2) NOT NULL,
        "Saldo" numeric(18,2) NOT NULL,
        "CreatedAt" timestamp with time zone,
        "CreatedBy" text,
        "LastModified" timestamp with time zone,
        "LastModifiedBy" text,
        CONSTRAINT "PK_Movimientos" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Movimientos_Cuentas_CuentaId" FOREIGN KEY ("CuentaId") REFERENCES "Cuentas" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260911215457_InitialCreate') THEN
    CREATE INDEX "IX_Cuentas_ClienteId" ON "Cuentas" ("ClienteId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260911215457_InitialCreate') THEN
    CREATE UNIQUE INDEX "IX_Cuentas_NumeroCuenta" ON "Cuentas" ("NumeroCuenta");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260911215457_InitialCreate') THEN
    CREATE INDEX "IX_Movimientos_CuentaId" ON "Movimientos" ("CuentaId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260911215457_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260911215457_InitialCreate', '8.0.11');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260912145134_ChangeAccountNumberLength') THEN
    ALTER TABLE "Cuentas" ALTER COLUMN "NumeroCuenta" TYPE character varying(15);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260912145134_ChangeAccountNumberLength') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260912145134_ChangeAccountNumberLength', '8.0.11');
    END IF;
END $EF$;
COMMIT;


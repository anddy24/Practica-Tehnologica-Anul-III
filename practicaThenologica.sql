CREATE DATABASE GestiuneAngajati;
USE GestiuneAngajati;

CREATE TABLE Angajati (
    CodAngajat INT PRIMARY KEY,
    Nume VARCHAR(50),
    Prenume VARCHAR(50),
    DataNasterii DATE,
    Sex CHAR(1),
    CodProfesie INT,
    Salariu DECIMAL(10, 2),
    StagiuMunca INT,
	FOREIGN KEY (CodProfesie) REFERENCES Profesii(CodProfesie) ON DELETE CASCADE
);

CREATE TABLE Departamente (
    CodDepartament INT PRIMARY KEY,
    Denumire VARCHAR(50)
);

CREATE TABLE Profesii (
    CodProfesie INT PRIMARY KEY,
    Denumire VARCHAR(50),
	CodDepartament INT,
	FOREIGN KEY (CodDepartament) REFERENCES Departamente(CodDepartament)
);

CREATE TABLE OreLucrate (
    CodAngajat INT,
    Luna INT,
    OreLucrate INT,
    PRIMARY KEY (CodAngajat, Luna), 
    FOREIGN KEY (CodAngajat) REFERENCES Angajati(CodAngajat) ON DELETE CASCADE
);

DROP TABLE OreLucrate;
DROP TABLE Profesii;
DROP TABLE Departamente;
DROP TABLE Angajati;

SELECT * FROM Angajati;

INSERT INTO Departamente (CodDepartament, Denumire) VALUES
(1, 'Departamentul de Vânzări'),
(2, 'Departamentul de Marketing'),
(3, 'Departamentul de IT'),
(4, 'Departamentul de Resurse Umane'),
(5, 'Departamentul Financiar');

INSERT INTO Profesii (CodProfesie, Denumire, CodDepartament) VALUES
(1, 'Vânzător', 1),
(2, 'Marketing Manager', 2),
(3, 'Programator', 3),
(4, 'Specialist HR', 4),
(5, 'Contabil', 5);

INSERT INTO Angajati (CodAngajat, Nume, Prenume, DataNasterii, Sex, CodProfesie, Salariu, StagiuMunca) VALUES
(1, 'Popescu', 'Ion', '1980-05-10', 'M', 1, 2500.00, 5),
(2, 'Ionescu', 'Maria', '1990-08-15', 'F', 2, 3000.00, 3),
(3, 'Constantinescu', 'Andrei', '1985-03-20', 'M', 3, 3500.00, 7),
(4, 'Dumitrescu', 'Elena', '1982-11-25', 'F', 1, 2700.00, 8),
(5, 'Popa', 'Mihai', '1995-01-30', 'M', 4, 3200.00, 4),
(6, 'Georgescu', 'Ana', '1987-07-05', 'F', 2, 2800.00, 6),
(7, 'Mihai', 'Gabriel', '1989-09-12', 'M', 3, 3800.00, 9),
(8, 'Neagu', 'Alexandra', '1992-04-18', 'F', 4, 2900.00, 5),
(9, 'Stoica', 'Cristian', '1983-06-22', 'M', 5, 3300.00, 7),
(10, 'Iancu', 'Ioana', '1991-10-08', 'F', 1, 3100.00, 6),
(11, 'Dinu', 'Andreea', '1988-02-14', 'F', 2, 3400.00, 8),
(12, 'Radu', 'Dan', '1986-12-01', 'M', 3, 3600.00, 10),
(13, 'Stan', 'Raluca', '1993-07-17', 'F', 4, 3000.00, 5),
(14, 'Florescu', 'Vlad', '1984-05-05', 'M', 5, 2800.00, 9),
(15, 'Gheorghiu', 'Laura', '1994-09-23', 'F', 1, 3200.00, 7),
(16, 'Voicu', 'Adrian', '1981-03-11', 'M', 2, 3300.00, 6),
(17, 'Marin', 'Cătălin', '1990-11-09', 'M', 3, 3500.00, 8),
(18, 'Stănescu', 'Elena', '1989-06-28', 'F', 4, 2900.00, 4),
(19, 'Dumitru', 'Ionuț', '1987-02-03', 'M', 5, 3100.00, 5),
(20, 'Munteanu', 'Andrei', '1986-08-19', 'M', 1, 3400.00, 6);

INSERT INTO OreLucrate (CodAngajat, Luna, OreLucrate) VALUES
(1, 1, 160),
(2, 1, 155),
(3, 1, 170),
(4, 1, 150),
(5, 1, 145),
(6, 1, 165),
(7, 1, 175),
(8, 1, 160),
(9, 1, 155),
(10, 1, 170),
(11, 1, 150),
(12, 1, 145),
(13, 1, 165),
(14, 1, 175),
(15, 1, 160),
(16, 1, 155),
(17, 1, 170),
(18, 1, 150),
(19, 1, 145),
(20, 1, 165);


--trigger pentru a urmari schibarile din tabelul angajati
CREATE TRIGGER trg_Angajati_AfterInsert
ON Angajati
AFTER INSERT
AS
BEGIN
    INSERT INTO Angajati_Log (CodAngajat, Nume, Prenume, DataNasterii, Sex, CodProfesie, Salariu, StagiuMunca, ActionDate, Action)
    SELECT CodAngajat, Nume, Prenume, DataNasterii, Sex, CodProfesie, Salariu, StagiuMunca, GETDATE(), 'INSERT'
    FROM inserted;
END

CREATE TABLE Angajati_Log (
    LogID INT IDENTITY(1,1) PRIMARY KEY,
    CodAngajat INT,
    Nume VARCHAR(50),
    Prenume VARCHAR(50),
    DataNasterii DATE,
    Sex CHAR(1),
    CodProfesie INT,
    Salariu DECIMAL(10, 2),
    StagiuMunca INT,
    ActionDate DATETIME,
    Action VARCHAR(50)
);

--prevenirea stergerii angajatului cu id 1
CREATE TRIGGER trg_Angajati_BeforeDelete
ON Angajati
INSTEAD OF DELETE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM deleted WHERE CodProfesie = 1)
    BEGIN
        RAISERROR('Cannot delete employees with CodProfesie 1.', 16, 1);
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        DELETE FROM Angajati WHERE CodAngajat IN (SELECT CodAngajat FROM deleted);
    END
END

--procedura pentru a inregistra un nou angajat
CREATE PROCEDURE AddNewEmployee
	@CodAngajat INT,
    @Nume VARCHAR(50),
    @Prenume VARCHAR(50),
    @DataNasterii DATE,
    @Sex CHAR(1),
    @CodProfesie INT,
    @Salariu DECIMAL(10, 2),
    @StagiuMunca INT
AS
BEGIN
    INSERT INTO Angajati (CodAngajat, Nume, Prenume, DataNasterii, Sex, CodProfesie, Salariu, StagiuMunca)
    VALUES (@CodAngajat, @Nume, @Prenume, @DataNasterii, @Sex, @CodProfesie, @Salariu, @StagiuMunca);
END

DROP PROCEDURE AddNewEmployee;

--procedura pentru a actualiza salariu
CREATE PROCEDURE UpdateEmployeeSalary
    @CodAngajat INT,
    @NewSalary DECIMAL(10, 2)
AS
BEGIN
    UPDATE Angajati
    SET Salariu = @NewSalary
    WHERE CodAngajat = @CodAngajat;
END

--procedura pentru a afisa angajatii dupa codul departamentului
CREATE PROCEDURE GetEmployeesByDepartment
    @CodDepartament INT
AS
BEGIN
    SELECT a.CodAngajat, a.Nume, a.Prenume, a.Salariu
    FROM Angajati a
    JOIN Profesii p ON a.CodProfesie = p.CodProfesie
    WHERE p.CodDepartament = @CodDepartament;
END

--funtctie pentru calcularea varstei angajatilor
CREATE FUNCTION CalculateAge(@DataNasterii DATE)
RETURNS INT
AS
BEGIN
    RETURN DATEDIFF(YEAR, @DataNasterii, GETDATE()) - 
           CASE WHEN MONTH(GETDATE()) < MONTH(@DataNasterii) OR 
                     (MONTH(GETDATE()) = MONTH(@DataNasterii) AND DAY(GETDATE()) < DAY(@DataNasterii)) 
                THEN 1 ELSE 0 END;
END


--Functie pentru a gasi departapentul in care lucreaza angajatul cu ID-ul dat de la tastatura
CREATE FUNCTION GetDepartmentName(@CodAngajat INT)
RETURNS VARCHAR(50)
AS
BEGIN
    DECLARE @DepartmentName VARCHAR(50);

    SELECT @DepartmentName = d.Denumire
    FROM Angajati a
    JOIN Profesii p ON a.CodProfesie = p.CodProfesie
    JOIN Departamente d ON p.CodDepartament = d.CodDepartament
    WHERE a.CodAngajat = @CodAngajat;

    RETURN @DepartmentName;
END

--functie pentru calcularea orelor totale de lucru a angajatului cu ID-ul dat de la tastatura
CREATE FUNCTION GetTotalWorkedHours(@CodAngajat INT)
RETURNS INT
AS
BEGIN
    DECLARE @TotalHours INT;

    SELECT @TotalHours = SUM(OreLucrate)
    FROM OreLucrate
    WHERE CodAngajat = @CodAngajat;

    RETURN @TotalHours;
END

--tabel utilizatotri

CREATE TABLE utilizatori (
    ID INT PRIMARY KEY IDENTITY(1,1),
    nume_utilizator VARCHAR(50) NOT NULL,
    parola VARCHAR(50) NOT NULL,
    tip_utilizator VARCHAR(20) CHECK (tip_utilizator IN ('admin', 'regular user'))
);

INSERT INTO utilizatori (nume_utilizator, parola, tip_utilizator) VALUES ('admin', 'securePassword123', 'admin');

INSERT INTO utilizatori (nume_utilizator, parola, tip_utilizator) VALUES ('Andrei', 'password456', 'regular user');

SELECT * FROM utilizatori;

--proceudra stocata pentru validarea si returnarea tipului de utilizator
CREATE PROCEDURE sp_VerifyUser
    @nume_utilizator VARCHAR(50),
    @parola VARCHAR(50)
AS
BEGIN
    SELECT tip_utilizator
    FROM utilizatori
    WHERE nume_utilizator = @nume_utilizator AND parola = @parola;
END

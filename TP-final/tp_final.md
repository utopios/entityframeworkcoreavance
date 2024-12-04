### TP Final

---

#### **Contexte**
Vous travaillez sur une plateforme universitaire où les performances des requêtes sont essentielles. 
---

### **Tables et script SQL**

Voici le script SQL pour créer la base de données et insérer des données initiales :

```sql
CREATE TABLE Students (
    StudentId INT PRIMARY KEY IDENTITY(1,1),
    LastName NVARCHAR(100) NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) UNIQUE NOT NULL,
    DateOfBirth DATE NULL,
    GPA DECIMAL(3,2) DEFAULT 0.0 CHECK (GPA BETWEEN 0 AND 4),
    DepartmentId INT NOT NULL
);

CREATE TABLE Professors (
    ProfessorId INT PRIMARY KEY IDENTITY(1,1),
    LastName NVARCHAR(100) NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    HireDate DATE NOT NULL,
    Salary DECIMAL(10,2) NOT NULL,
    DepartmentId INT NOT NULL
);

CREATE TABLE Departments (
    DepartmentId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) UNIQUE NOT NULL,
    Budget DECIMAL(18,2) NOT NULL
);

CREATE TABLE Courses (
    CourseId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(200) NOT NULL,
    Credits INT NOT NULL,
    DepartmentId INT NOT NULL
);

CREATE TABLE StudentCourses (
    StudentId INT NOT NULL,
    CourseId INT NOT NULL,
    Grade DECIMAL(3,2) NULL CHECK (Grade BETWEEN 0 AND 4),
    EnrollmentDate DATE NOT NULL,
    PRIMARY KEY (StudentId, CourseId)
);

```

---


#### **Partie 1 : Chargement et benchmarking**
1. Implémentez trois stratégies de chargement (lazy, eager, explicite) pour récupérer les étudiants inscrits à des cours, avec les données des professeurs associés.
2. Effectuez un benchmark des performances pour comparer les trois stratégies.
   - Mesurez le temps d'exécution et le nombre de requêtes SQL générées.


---

#### **Partie 2 Requêtes complexes**
1. Créez une requête LINQ pour :
   - Afficher tous les étudiants ayant une moyenne (GPA) supérieure à 3.5.
   - Inclure les cours auxquels ils sont inscrits et les professeurs responsables.
2. Modifiez cette requête pour qu’elle utilise une projection optimisée (DTO) et expliquez pourquoi cela améliore les performances.

---

#### **Partie 3 Recherche avancée avec UDF**
1. Implémentez une fonction SQL définie par l’utilisateur (UDF) pour calculer le salaire moyen des professeurs par département.
   ```sql
   CREATE FUNCTION GetAverageProfessorSalary(@DepartmentId INT)
   RETURNS DECIMAL(10,2)
   AS
   BEGIN
       RETURN (SELECT AVG(Salary) FROM Professors WHERE DepartmentId = @DepartmentId);
   END;
   ```
2. Intégrez cette fonction dans Entity Framework Core et affichez les départements avec leur budget et le salaire moyen des professeurs.

---

#### **Partie 4 Requêtes dynamiques**
1. Implémentez une méthode pour rechercher les cours en fonction :
   - Du nombre d’inscriptions.
   - D’un mot-clé contenu dans le titre.
   - De la plage de crédits (par exemple, entre 3 et 5).
2. Testez cette méthode avec différentes combinaisons de filtres.

---

#### **Partie 5: Recherche textuelle avancée**
1. Ajoutez un index full-text sur le titre des cours.
   ```sql
   CREATE FULLTEXT INDEX ON Courses (Title)
   KEY INDEX PK_Courses;
   ```
2. Implémentez une recherche qui retourne les cours correspondant à un mot-clé approximatif et ordonnez les résultats par pertinence.

---

#### **Partie 6 : Optimisation des transactions**
1. Implémentez une transaction pour les étapes suivantes :
   - Augmenter de 10% le budget du département "Computer Science".
   - Inscrire un étudiant à deux cours supplémentaires.
   - Si une erreur survient, annulez toutes les opérations.

---

#### **Partie 7 Analyse des requêtes SQL générées**
1. Utilisez les outils de suivi d’EF Core (`ToQueryString` et logs) pour analyser les requêtes générées par une opération complexe.
2. Identifiez les améliorations possibles pour minimiser les appels SQL et optimisez votre code.


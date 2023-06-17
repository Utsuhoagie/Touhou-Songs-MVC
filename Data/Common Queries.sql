-- ====== Setup New DB ========

-- CREATE DATABASE ImageTagger;

-- ====== Auth ==========

/* SELECT * FROM dbo.AspNetRoleClaims

SELECT * FROM dbo.AspNetRoles

SELECT * FROM dbo.AspNetUserClaims

SELECT * FROM dbo.AspNetUserLogins

SELECT * FROM dbo.AspNetUserRoles

SELECT * FROM dbo.AspNetUserTokens

SELECT [FullName], [UserName], Users.[Email], Roles.[Name] AS [RoleName], /* [PasswordHash],  *[RefreshToken], [RefreshTokenExpiryTime] FROM (
    dbo.AspNetUsers AS Users
        LEFT JOIN
    dbo.People AS People
        ON Users.EmployeeId = People.Id
        
        LEFT JOIN
    dbo.AspNetUserRoles AS UserRoles
        ON Users.Id = UserRoles.UserId

        LEFT JOIN
    dbo.AspNetRoles AS Roles
        ON Roles.Id = UserRoles.RoleId );
 */

-- ====== App ============

-- Get Images
SELECT * FROM dbo.Images;

-- Get Tags
SELECT * FROM dbo.Tags;


-- Get Images & Tags
SELECT * FROM dbo._JOIN_ImageTag;

SELECT [ImageId], [TagId], [Title], [Name] AS [TagName] FROM 
    dbo.[_JOIN_ImageTag]
        LEFT JOIN dbo.Images ON dbo.Images.Id = dbo.[_JOIN_ImageTag].ImageId
        LEFT JOIN dbo.Tags ON dbo.Tags.Id = dbo.[_JOIN_ImageTag].TagId;



-- DELETE FROM dbo.Images
-- WHERE 1=1;

-- ========= Renaming =================

-- EXEC sp_rename 'dbo.Images.ImageId', 'Id', 'COLUMN';
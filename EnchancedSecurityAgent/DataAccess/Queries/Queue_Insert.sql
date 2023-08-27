INSERT INTO QE.UserSecurityQueue (UserArtifactID)
SELECT 
	u.ArtifactID
FROM EDDS.EDDSDBO.[User] u WITH (NOLOCK)
LEFT JOIN EDDS.QE.UserSecurityQueue usq WITH (NOLOCK)
ON	usq.UserArtifactID = u.ArtifactID
WHERE  
	u.RelativityAccess = 1
AND 
	SUBSTRING(u.EmailAddress,CHARINDEX('@',u.EmailAddress,0)+1,(LEN(u.EmailAddress) - CHARINDEX('@',u.EmailAddress,0))) 
	NOT IN ('kcura.com','PreviewUser.com','relativity.com')
AND
	DATEDIFF(d,u.LastLoginDate,GETUTCDATE()) > @NumofDays
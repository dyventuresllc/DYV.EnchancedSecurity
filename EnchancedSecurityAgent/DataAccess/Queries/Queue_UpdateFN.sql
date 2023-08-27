UPDATE	qe.UserSecurityQueue
SET		[FirstNotificationDateSent] = GETUTCDATE()
WHERE	[UserArtifactID] = @UserArtifactID
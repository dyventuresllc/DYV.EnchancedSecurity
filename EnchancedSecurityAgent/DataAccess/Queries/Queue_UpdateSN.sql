UPDATE	qe.UserSecurityQueue
SET		[SecondNotificationDateSent] = GETUTCDATE()
WHERE	[UserArtifactID] = @UserArtifactID
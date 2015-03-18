using System;
using System.Reflection;
using System.Collections.Generic;

namespace GXPEngine
{
	//------------------------------------------------------------------------------------------------------------------------
	//														CollisionManager
	//------------------------------------------------------------------------------------------------------------------------
	public class CollisionManager
	{
		
		private delegate void CollisionDelegate(GameObject gameObject);
		
		//------------------------------------------------------------------------------------------------------------------------
		//														ColliderInfo
		//------------------------------------------------------------------------------------------------------------------------
		private struct ColliderInfo {
			public GameObject gameObject;
			public CollisionDelegate onCollision;
			
			//------------------------------------------------------------------------------------------------------------------------
			//														ColliderInfo()
			//------------------------------------------------------------------------------------------------------------------------
			public ColliderInfo(GameObject gameObject, CollisionDelegate onCollision) {
				this.gameObject = gameObject;
				this.onCollision = onCollision;
			}
		}
	
		private List<GameObject> colliderList = new List<GameObject>();
		private List<ColliderInfo> activeColliderList = new List<ColliderInfo>();
		private Dictionary<GameObject, ColliderInfo> _collisionReferences = new Dictionary<GameObject, ColliderInfo>();
				
		//------------------------------------------------------------------------------------------------------------------------
		//														CollisionManager()
		//------------------------------------------------------------------------------------------------------------------------
		public CollisionManager ()
		{
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														Step()
		//------------------------------------------------------------------------------------------------------------------------
		public void Step() {
			for (int i=activeColliderList.Count-1; i>= 0; i--) {
				ColliderInfo info = activeColliderList[i];
				for (int j=colliderList.Count-1; j>=0; j--) {
					if (j >= colliderList.Count) continue; //fix for removal in loop
					GameObject other = colliderList[j];
					if (info.gameObject != other) {
						if (info.gameObject.HitTest(other)) {
							if (info.onCollision != null) {
								info.onCollision(other);
							}
						}
					}
				}
			}
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														Add()
		//------------------------------------------------------------------------------------------------------------------------
		public void Add(GameObject gameObject) {
			if (gameObject.collider != null && !colliderList.Contains (gameObject)) {
				colliderList.Add(gameObject);
			}

			MethodInfo info = gameObject.GetType().GetMethod("OnCollision", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

			if (info != null) {

				CollisionDelegate onCollision = (CollisionDelegate)Delegate.CreateDelegate(typeof(CollisionDelegate), gameObject, info, false);
				if (onCollision != null && !_collisionReferences.ContainsKey (gameObject)) {
					ColliderInfo colliderInfo = new ColliderInfo(gameObject, onCollision);
					_collisionReferences[gameObject] = colliderInfo;
					activeColliderList.Add(colliderInfo);
				}

			} else {
				validateCase(gameObject);
			}
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														validateCase()
		//------------------------------------------------------------------------------------------------------------------------
		private void validateCase(GameObject gameObject) {
			MethodInfo info = gameObject.GetType().GetMethod("OnCollision", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
			if (info != null) {
				throw new Exception("'OnCollision' function was not binded. Please check it's correct case (capital O?)");
			}
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														Remove()
		//------------------------------------------------------------------------------------------------------------------------
		public void Remove(GameObject gameObject) {
			colliderList.Remove(gameObject);
			if (_collisionReferences.ContainsKey(gameObject)) {
				ColliderInfo colliderInfo = _collisionReferences[gameObject];
				activeColliderList.Remove(colliderInfo);
				_collisionReferences.Remove(gameObject);
			}
		}
		
	}
}


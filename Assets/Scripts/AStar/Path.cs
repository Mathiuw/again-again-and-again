using UnityEngine;

namespace MaiNull.AStar
{
    public class Path
    {
        public readonly Vector3[] LookPoints;
        public readonly Line[] TurnBoundaries;
        public readonly int FinishLineIndex;
        public readonly int SlowDownIndex;
        
        public Path(Vector3[] waypoints, Vector3 startPosition, float turnDist, float stoppingDist)
        {
            LookPoints = waypoints;
            TurnBoundaries = new Line[LookPoints.Length];
            FinishLineIndex = TurnBoundaries.Length - 1;
            
            Vector2 previousPoint = V3ToV2(startPosition);
            for (int i = 0; i < LookPoints.Length; i++)
            {
                Vector2 currentPoint = V3ToV2(LookPoints[i]);
                Vector2 dirToCurrentPoint = (currentPoint - previousPoint).normalized;
                Vector2 turnBoundaryPoint = (i == FinishLineIndex) ? currentPoint : currentPoint - dirToCurrentPoint * turnDist;
                TurnBoundaries[i] = new Line(turnBoundaryPoint, previousPoint - dirToCurrentPoint * turnDist);
                previousPoint = turnBoundaryPoint;
            }

            float distFromEndPoint = 0;
            for (int i = LookPoints.Length - 1; i > 0; i--)
            {
                distFromEndPoint += Vector3.Distance(LookPoints[i], LookPoints[i - 1]);
                if (!(distFromEndPoint > stoppingDist)) continue;
                SlowDownIndex = i;
                break;
            }
            
        }
        
        private Vector2 V3ToV2(Vector3 v3)
        {
            return new Vector2(v3.x, v3.z);
        }

        public void DrawWithGizmos()
        {
            Gizmos.color = Color.black;
            foreach (Vector3 point in LookPoints)    
            {
                Gizmos.DrawSphere(point + Vector3.up, .3f);
            }

            for (int i = 0; i < LookPoints.Length; i++)
            {
                if (i == LookPoints.Length-1) continue;
                Gizmos.DrawLine(LookPoints[i], LookPoints[i+1]);
            }
            
            Gizmos.color = Color.white;
            foreach (Line line in TurnBoundaries)
            {
                line.DrawWithGizmos(3);
            }
        }
    }
}
using UnityEngine;

namespace MaiNull.AStar
{
    public struct Line
    {
        private const float VerticalLineGradient = 1e5f;
        
        private float _gradient;
        private float _yIntercept;
        private Vector2 _pointOnLine1;
        private Vector2 _pointOnLine2;
        private bool _approachSide;
        
        private float _gradientPerpendicular;
        
        public Line(Vector2 pointOnLine, Vector2 pointPerpendicularToLine)
        {
            float deltaX = pointOnLine.x - pointPerpendicularToLine.x;
            float deltaY = pointOnLine.y - pointPerpendicularToLine.y;

            if (deltaX == 0)
            {
                _gradientPerpendicular = VerticalLineGradient;
            }
            else _gradientPerpendicular = deltaY / deltaX;
            
            if (_gradientPerpendicular == 0)
            {
                _gradient = VerticalLineGradient;
            }
            else _gradient = -1 / _gradientPerpendicular;
            
            _yIntercept = pointOnLine.y - _gradient * pointOnLine.x;
            _pointOnLine1 = pointOnLine;
            _pointOnLine2 = pointOnLine + new Vector2(1, _gradient);

            _approachSide = false;
            _approachSide = GetSide(pointPerpendicularToLine);
        }

        private bool GetSide(Vector2 point)
        {
            return (point.x - _pointOnLine1.x) * (_pointOnLine2.y - _pointOnLine1.y) > 
                   (point.y - _pointOnLine1.y) * (_pointOnLine2.x - _pointOnLine1.x);
        }
        
        public bool HasCrossedLine(Vector2 point)
        {
            return GetSide(point) != _approachSide;
        }

        public float DistanceFromPoint(Vector2 point)
        {
            float yInterceptPerpendicular = point.y - _gradientPerpendicular * point.x;
            float intersectX = (yInterceptPerpendicular - _yIntercept) / (_gradient - _gradientPerpendicular);
            float intersectY = _gradient * intersectX + _yIntercept;
            return Vector2.Distance(point, new Vector2(intersectX, intersectY));
        }
        
        public void DrawWithGizmos(float length)
        {
            Vector3 lineDir = new Vector3(1, 0, _gradient).normalized;
            Vector3 lineCenter = new Vector3(_pointOnLine1.x, 0, _pointOnLine1.y) + Vector3.up;
            Gizmos.DrawLine(lineCenter - lineDir * length/2, lineCenter + lineDir * length/2);
        }
    }
}
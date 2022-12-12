using UnityEngine;

	public class CameraMovement : MonoBehaviour {

		[Header("Camera Movement:")]
		[SerializeField] private float _movementSpeed = 10f; 
        [SerializeField] private float _offsetZ = 10; // отступ по оси z

		//Границы перемешения камеры
		[SerializeField] private bool _screenLimit = false;
		[SerializeField] private Vector2 _minPos; 
		[SerializeField] private Vector2 _maxPos;
    
        private bool _moved = false; //Двигаеться ли камера ?

		private void FixedUpdate () 
		{
                MoveCam();

        //Ограничения положения камеры, если камера переместилась за границы
        transform.position = RefinePosition(transform.position);

            _moved = false;
        }


        private void MoveCam ()
        {

            Vector3 TargetMvt = Vector3.zero;

        //если есть направление, в котором нужно двигаться
        if (TargetMvt != Vector3.zero)
            {
                TargetMvt *= _movementSpeed * Time.deltaTime;
                TargetMvt = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * TargetMvt;

                transform.Translate(TargetMvt, Space.World);

                _moved = true;
            }
            else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
            { //движение с помощью клавиатуры
                transform.Translate(Vector3.right.normalized * Input.GetAxis("Horizontal") * _movementSpeed * Time.deltaTime);
                transform.Translate((Vector3.up + Vector3.forward).normalized * Input.GetAxis("Vertical") * _movementSpeed * Time.deltaTime);

                _moved = true;
            }
        }

    // двидение камеры к определенной точке
    public void MovePosition(Transform target)
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.position.x, _movementSpeed * Time.deltaTime), transform.position.y, Mathf.Lerp(transform.position.z, target.position.z - _offsetZ, _movementSpeed * Time.deltaTime));
    }

    //уточняет положение в соответствии с настройками камеры
    Vector3 RefinePosition (Vector3 Position)
        {
            //если ограничения сторон включены
            if (_screenLimit == true)
            {
                Position = new Vector3(Mathf.Clamp(Position.x, _minPos.x, _maxPos.x), Position.y, Mathf.Clamp(Position.z, _minPos.y, _maxPos.y));
            }

            return Position;
        }

    }
using UnityEngine;
using System.Collections;

public class PonyControllerScript : MonoBehaviour
{
    //переменная для установки макс. скорости персонажа
    public float maxSpeed = 10f;
    //переменная для определения направления персонажа вправо/влево
    private bool isFacingRight = true;
    //ссылка на компонент анимаций
    private Animator anim;

    /// <summary>
    /// Начальная инициализация
    /// </summary>
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Выполняем действия в методе FixedUpdate, т. к. в компоненте Animator персонажа
    /// выставлено значение Animate Physics = true и анимация синхронизируется с расчетами физики
    /// </summary>
    private void FixedUpdate()
    {
        //используем Input.GetAxis для оси Х. метод возвращает значение оси в пределах от -1 до 1.
        //при стандартных настройках проекта 
        //-1 возвращается при нажатии на клавиатуре стрелки влево (или клавиши А),
        //1 возвращается при нажатии на клавиатуре стрелки вправо (или клавиши D)
        float move = Input.GetAxis("Horizontal");
        //в компоненте анимаций изменяем значение параметра Speed на значение оси Х.
        //приэтом нам нужен модуль значения
        anim.SetFloat("Speed", Mathf.Abs(move));

        //обращаемся к компоненту персонажа RigidBody2D. задаем ему скорость по оси Х, 
        //равную значению оси Х умноженное на значение макс. скорости
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = new Vector2(move * maxSpeed, rb2D.velocity.y);

        //если нажали клавишу для перемещения вправо, а персонаж направлен влево
        if (move > 0 && !isFacingRight)
        {
            //anim.SetBool("GoToRight",true);
            ////отражаем персонажа вправо
            //anim.SetBool("GoToLeft", false);
            Flip();
        }
           
        //обратная ситуация. отражаем персонажа влево
        else if (move < 0 && isFacingRight)
        {
            
            //anim.SetBool("GoToRight", false);
            //anim.SetBool("GoToLeft", true);
            Flip();
        }
    }

    /// <summary>
    /// Метод для смены направления движения персонажа и его зеркального отражения
    /// </summary>
    private void Flip()
    {
        Debug.Log("private void Flip()");
        if (isFacingRight)
            anim.CrossFade("Pony_Turn",1);
        else
            anim.CrossFade("Pony_Turn",1);
        //меняем направление движения персонажа
        isFacingRight = !isFacingRight;
        //получаем размеры персонажа
        //Vector3 theScale = transform.localScale;
        ////зеркально отражаем персонажа по оси Х
        //theScale.x *= -1;
        ////задаем новый размер персонажа, равный старому, но зеркально отраженный
        //transform.localScale = theScale;
    }
}
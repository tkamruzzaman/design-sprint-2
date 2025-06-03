using DG.Tweening;
using UnityEngine;

public class Draging : MonoBehaviour
{
    [SerializeField] bool isDraging;
    Vector3 initialScale;
    [SerializeField] float selectionScale = 1.1f;
    [SerializeField] SpriteRenderer playerSprite;
    private ComicPanel currentPanel; // Track current panel


    void Awake()
    {
        initialScale = playerSprite.transform.localScale;
    }

    void OnMouseDown()
    {
        isDraging = true;
        playerSprite.transform.DOScale(initialScale * selectionScale, 0.5f);
    }

    private void OnMouseUp()
    {
        isDraging = false;
        playerSprite.transform.DOScale(initialScale, 0.2f);

            // Snap to panel center if dropped on one
        if (currentPanel != null && currentPanel.CurrentState == ComicPanelState.Active && 
            !currentPanel.IsPlayingContent)
        {
            // Snap to spawn position center
            transform.DOMove(currentPanel.spawnTransform.position, 0.3f);
            // Start the panel's content (animations + dialogue)
            currentPanel.StartContent();
        }
    }

    void Update()
    {
        if (isDraging)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

  void OnTriggerEnter2D(Collider2D collision)
    {
        ComicPanel panel = collision.GetComponent<ComicPanel>();
        if (panel != null)
        {
            currentPanel = panel;
            if (panel.CurrentState == ComicPanelState.Inactive)
            {
                panel.Active();
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        ComicPanel panel = collision.GetComponent<ComicPanel>();
        if (panel != null && panel == currentPanel)
        {
            currentPanel = null;
        }
    }
}
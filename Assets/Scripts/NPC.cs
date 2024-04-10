using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    [SerializeField] private TextMesh text;
    [SerializeField] private TextMesh description;
    [SerializeField] private string sceneName;

    private bool focused = false;

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer meshRenderer = text.GetComponent<MeshRenderer>();
        MeshRenderer meshRendererDescription = description.GetComponent<MeshRenderer>();
        meshRendererDescription.enabled = false;
        meshRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (focused && Input.GetKeyDown(KeyCode.Space) && sceneName != "")
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MeshRenderer meshRenderer = text.GetComponent<MeshRenderer>();
            MeshRenderer meshRendererDescription = description.GetComponent<MeshRenderer>();
            meshRendererDescription.enabled = true;
            meshRenderer.enabled = true;

            focused = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MeshRenderer meshRenderer = text.GetComponent<MeshRenderer>();
            MeshRenderer meshRendererDescription = description.GetComponent<MeshRenderer>();
            meshRendererDescription.enabled = false;
            meshRenderer.enabled = false;
            focused = false;
        }
    }
}

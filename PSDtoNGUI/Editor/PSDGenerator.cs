using UnityEngine;
using System.Collections;
using PhotoshopFile;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace PSDGenerator
{
    public static class PSDUtils
    {
        public static PsdFile ImportPSD(string path)
        {
            PsdFile psd = null;
            psd = new PsdFile(path, Encoding.Default);
            Debug.Log("Import PSD File " + Path.GetFileNameWithoutExtension(path));
            return psd;
        }

        public static void Generator(PsdFile psd, UIAtlas uiAtlas, string rootName)
        {
            Debug.Log("Generate Start!");
            Debug.Log(psd.BaseLayer.Rect.width + "  " + psd.BaseLayer.Rect.height);
            GameObject root = null;
            if (rootName == null || (root = GameObject.Find(rootName)) == null)
            {
                Debug.LogError("Root not found!");
                return;
            }
            foreach (Layer layer in psd.Layers)
            {
                if (layer.Name != "</Layer set>" &&
                    layer.Name != "</Layer group>" &&
                    layer.Visible &&
                    layer.Rect.width > 0 &&
                    layer.Rect.height > 0)
                {
                    Debug.Log(layer.Name);
                    Debug.Log(layer.Rect.width + "  " + layer.Rect.height);
                    string pattern = "label_";
                    Regex regexInstance = new Regex(pattern);
                    if (regexInstance.IsMatch(layer.Name))
                    {
                        UILabel label = NGUITools.AddWidget<UILabel>(root, 1);
                        label.pivot = UIWidget.Pivot.TopLeft;
                        Vector3 spritePos = new Vector3(layer.Rect.position.x - psd.BaseLayer.Rect.width / 2, psd.BaseLayer.Rect.height / 2 - layer.Rect.position.y);
                        Debug.Log("before: " + layer.Rect.position + " after: " + spritePos);
                        label.name = layer.Name;
                        label.width = (int)layer.Rect.width;
                        label.height = (int)layer.Rect.height;
                        label.transform.localPosition = spritePos;
                        label.pivot = UIWidget.Pivot.Center;
                    }
                    else
                    {
                        UISprite sprite = NGUITools.AddSprite(root, uiAtlas, layer.Name, 1);
                        sprite.pivot = UIWidget.Pivot.TopLeft;
                        Vector3 spritePos = new Vector3(layer.Rect.position.x - psd.BaseLayer.Rect.width / 2, psd.BaseLayer.Rect.height / 2 - layer.Rect.position.y);
                        Debug.Log("before: " + layer.Rect.position + " after: " + spritePos);
                        sprite.name = layer.Name;
                        sprite.width = (int)layer.Rect.width;
                        sprite.height = (int)layer.Rect.height;
                        sprite.transform.localPosition = spritePos;
                        sprite.pivot = UIWidget.Pivot.Center;
                        sprite.type = UISprite.Type.Sliced;
                    }
                }
            }
        }
    }
}
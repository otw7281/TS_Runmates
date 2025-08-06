// Vers�o simplificada e mais robusta do shader "Sempre por Cima"
Shader"Unlit/AlwaysOnTopShader"
{
    // Propriedades que aparecer�o no Inspector do Material
    Properties
    {
        // A textura do nosso �cone
        _MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
        // A cor do componente Image da UI, para que possamos mudar a cor do �cone se quisermos
        _Color ("Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        // Tags para garantir a ordem de renderiza��o e transpar�ncia corretas
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }

        Pass
        {
            // --- A L�GICA PRINCIPAL E SIMPLIFICADA ---

            // Ignora o teste de profundidade. Sempre renderiza.
ZTest Always

            // Habilita a transpar�ncia
Blend SrcAlpha
OneMinusSrcAlpha

            // N�o escreve no Z-Buffer (importante para transpar�ncia)
            ZWrite
Off

            // N�o remove pol�gonos traseiros (bom para planos simples)
            Cull
Off

            // Combina a cor do componente Image com a textura
            SetTexture[_MainTex]
{
    ConstantColor[_Color]
                Combine
    Texture * Constant

}
        }
    }
}
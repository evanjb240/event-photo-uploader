<template>
    <div>
        <NavBar />
        <div class="main">
            <div class="container">
                <slot></slot>
            </div>
            <footer>
            </footer>
        </div>
    </div>
</template>
<style>
.main{
    display: flex;
    justify-content: center;
    margin-top:50px;
    width:100%;
    height:100%;
}
.container{
    margin-top:10px;
    margin-bottom:50px;
    border-radius: 5px 5px 0px 0px;
    width:90%;
    background-color: whitesmoke;
    padding:20px;
}
footer{
    position: fixed;
    bottom: 0;
    left: 0;
    right: 0;
    background: #333;
    height: auto;
    width: 100vw;
    padding-top: 40px;
    color: #fff;
}
.centered{
  display:flex;
  justify-content: center;
  text-align: center;
  align-items: center;
}
h1 {
	font-family: 'Noto EC Light';
	font-size: 60px;
	margin-bottom: 30px;
}
h2,h3,nav {
	font-family: 'Barlow CR';
}
p,li, div{
    font-family: 'Barlow CR';
    font-size: 20px;
}
</style>
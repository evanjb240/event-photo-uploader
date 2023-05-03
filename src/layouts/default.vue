<template>
    <div>
        <NavBar />
        <div class="main">
            <div class="container">
                <slot></slot>
            </div>
        </div>
    </div>
</template>
<style scoped>
.main{
    display: flex;
    justify-content: center;
    margin-top:50px;
    width:100%;
    height:100%;
}
.container{
    margin-top:10px;
    border-radius: 5px 5px 0px 0px;
    width:90%;
    background-color: whitesmoke;
    padding:20px;
}
</style>
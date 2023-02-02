<template>
    <div>
        <NavBar />
        <div class="main">
            <slot />
        </div>
    </div>
</template>
<style scoped>
.main{
    margin-top:50px;
}
</style>
<script>
import { MenuIcon } from '@zhuowenli/vue-feather-icons'

export default {
    data() {
        return {
            nav: "topnav"
        }
    },
    mounted() {
    },
    components: {
        MenuIcon
    },
    methods: {
        navBar() {
            if (this.nav === "topnav") {
                this.nav += ' responsive';

            } else {
                this.nav = 'topnav';
            }
        }
    }
}
</script>

<template>
    <nav :class="nav">
        <NuxtLink to="/" @click="nav = 'topnav'">Photo Uploader</NuxtLink>
        <NuxtLink to="/" @click="nav = 'topnav'">Home</NuxtLink>
        <NuxtLink to="/Uploader" @click="nav = 'topnav'">Photo Upload</NuxtLink>
        <NuxtLink to="Privacy" @click="nav = 'topnav'">Privacy</NuxtLink>
        <a href="javascript:void(0);" class="icon">
            <menu-icon size="1.5x" @click="navBar"></menu-icon>
        </a>
    </nav>
</template>

<style>
body {
    margin: 0;
    font-family: Arial, Helvetica, sans-serif;
}

.topnav {
    overflow: hidden;
    background-color: #333;
}

.topnav a {
    float: left;
    display: block;
    color: #f2f2f2;
    text-align: center;
    padding: 14px 16px;
    text-decoration: none;
    font-size: 17px;
}

.topnav a:hover {
    background-color: #ddd;
    color: black;
}

.topnav a.active {
    background-color: #04AA6D;
    color: white;
}

.topnav .icon {
    display: none;
}

@media screen and (max-width: 600px) {
    .topnav a:not(:first-child) {
        display: none;
    }

    .topnav a.icon {
        float: right;
        display: block;
    }
}

@media screen and (max-width: 600px) {
    .topnav.responsive {
        position: relative;
    }

    .topnav.responsive .icon {
        position: absolute;
        right: 0;
        top: 0;
    }

    .topnav.responsive a {
        float: none;
        display: block;
        text-align: left;
    }
}
</style>
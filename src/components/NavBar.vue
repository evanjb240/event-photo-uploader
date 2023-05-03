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
    <nav class="nav" :class="nav">
        <NuxtLink to="/" @click="nav = 'topnav'">Photo Uploader</NuxtLink>
        <NuxtLink to="/" @click="nav = 'topnav'">Home</NuxtLink>
        <NuxtLink to="/Venue" @click="nav = 'topnav'">Venue</NuxtLink>
        <NuxtLink to="/RSVP" @click="nav = 'topnav'">RSVP</NuxtLink>
        <NuxtLink to="/WeddingParty" @click="nav = 'topnav'">Wedding Party</NuxtLink>
        <NuxtLink to="/Travel" @click="nav = 'topnav'">Travel</NuxtLink>
        <NuxtLink to="/Guidelines" @click="nav = 'topnav'">Guidelines</NuxtLink>
        <NuxtLink to="/GiftRegistry" @click="nav = 'topnav'">Gift Registry</NuxtLink>
        <NuxtLink to="/Uploader" @click="nav = 'topnav'">Photo Upload</NuxtLink>
        <a href="javascript:void(0);" @click="navBar" class="icon">
            <menu-icon size="16"></menu-icon>
        </a>
    </nav>
</template>

<style>
body {
    margin: 0;
    font-family: Arial, Helvetica, sans-serif;
}

.nav{
    position:fixed;
    min-height:50px;
    top:0;
    width:100%;
    z-index: 99;
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
    padding: 10px 12px;
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

@media screen and (max-width: 1000px) {
    .topnav a:not(:first-child) {
        display: none;
    }

    .topnav a.icon {
        float: right;
        display: block;
    }
}

@media screen and (max-width: 1000px) {
    .topnav.responsive {
        position: fixed;
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
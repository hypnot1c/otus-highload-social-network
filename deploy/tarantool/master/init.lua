#!/usr/bin/env tarantool

-- Add Taranrocks pathes. https://github.com/rtsisyk/taranrocks/blob/master/README.md
local home = os.getenv("HOME")
package.path = [[/usr/local/share/tarantool/lua/?/init.lua;]]..package.path
package.path = [[/usr/local/share/tarantool/lua/?.lua;]]..package.path
package.path = home..[[/.tarantool/share/tarantool/lua/?/init.lua;]]..package.path
package.path = home..[[/.tarantool/share/tarantool/lua/?.lua;]]..package.path
package.cpath = [[/usr/local/lib/tarantool/lua/?.so;]]..package.cpath
package.cpath = home..[[/.tarantool/lib/tarantool/lua/?.so;]]..package.cpath

local log = require('log')

box.cfg
{
    pid_file = nil,
    background = false,
    log_level = 5
}

log.info("BEGINS!")

local function init()
    box.schema.user.create('web_api', {password = 'password', if_not_exists = true})
    log.info("User created!")
    box.schema.user.grant('web_api', 'read,write,execute', 'universe', nil, { if_not_exists = true })
    log.info("Access granted!")

    some_space = box.schema.space.create('user_dialog', { format = {
        {name = 'id', type = 'string'},
        {name = 'fromUserId', type = 'integer'},
        {name = 'toUserId', type = 'integer'},
        {name = 'text', type = 'string'},
        {name = 'createdAt', type = 'string'},
    }})
    log.info(some_space.name .. " space was created.")

    some_space:create_index('primary', {
        if_not_exists = true,
        type = 'TREE',
        unique = true,
        parts = {'id'}
    })

    some_space:create_index('secondary', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {'fromUserId', 'toUserId'}
    })
end

box.once('init', init)
